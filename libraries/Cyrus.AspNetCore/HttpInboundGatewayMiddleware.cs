using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.IO;

namespace Cyrus.AspNetCore
{
    public class HttpInboundGatewayMiddleware : IInboundGateway
    {
        private readonly RequestDelegate _next;

        public IReceiveChannel InboundChannel { get; }
        public ISendChannel OutboundChannel { get; }

        public HttpInboundGatewayMiddleware(
            RequestDelegate next,
            IReceiveChannel requestChannel,
            ISendChannel replyChannel)
        {
            OutboundChannel = replyChannel;
            InboundChannel = requestChannel;
            _next = next;            
        }        

        public async Task Invoke(HttpContext context)
        {
            var originalRequestBody = context.Request.Body;
            var requestStream = new MemoryStream();
            var messageStream = new MemoryStream();

            var bytesRead = 0;
            var buffer = new byte[1024];
            do
            {
                bytesRead = await originalRequestBody.ReadAsync(buffer, 0, buffer.Length);
                var requestStreamWriteTask = requestStream.WriteAsync(buffer, 0, bytesRead);
                var messageStreamWriteTask = messageStream.WriteAsync(buffer, 0, bytesRead);
                await Task.WhenAll(requestStreamWriteTask, messageStreamWriteTask);

            } while (bytesRead > 0);
            
            requestStream.Seek(0, SeekOrigin.Begin);
            messageStream.Seek(0, SeekOrigin.Begin);

            var message = new Message(messageStream, GetMessageHeaders(context.Request));
            await OutboundChannel.SendAsync(message);
            
            context.Request.Body = requestStream;

            await _next(context);
            context.Request.Body = originalRequestBody;
        }

        private IDictionary<string, string> GetMessageHeaders(HttpRequest request)
        {
            return new Dictionary<string, string> { };
        }

        public Task ReceiveAsync()
        {
            throw new NotImplementedException();
        }
    }
}
