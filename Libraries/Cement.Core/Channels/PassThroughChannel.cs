using Cement.IO;
using Cement.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cement.Channels
{
    public class PassThroughChannel : IChannel
    {
        private IMessageFactory messageFactory;
        private IList<IMessageHandler> messageHandlerList = new List<IMessageHandler>();

        public PassThroughChannel(IMessageFactory messageFactory)
        {
            this.messageFactory = messageFactory;
        }

        public void Publish(Messages.IMessage message)
        {   
            var handlersAndMessages = messageHandlerList
                .Select(handler =>  new { Handler=handler, Message=messageFactory.Create() })
                .ToList();
            
            int bytesRead = 0;
            byte[] buffer= new byte[1024];
            do
            {
                int count = buffer.Length - bytesRead;
                bytesRead = message.Body.Read(buffer, 0, count);
                foreach (var outputMessage in handlersAndMessages.Select(x=>x.Message))
                    outputMessage.Body.Write(buffer, 0, bytesRead);
            } while (bytesRead > 0);

            foreach (var tuple in handlersAndMessages)
                tuple.Handler.HandleMessage(tuple.Message);
        }

        public Task PublishAsync(IMessage message)
        {
            return PublishAsync(message, CancellationToken.None);
        }

        public async Task PublishAsync(IMessage message, CancellationToken cancellationToken)
        {
            var currentCacnellationToken = CancellationToken.None;
            if (!cancellationToken.Equals(currentCacnellationToken))
                currentCacnellationToken = cancellationToken;

            var handlersAndMessages = messageHandlerList
                .Select(handler => new { Handler = handler, Message = messageFactory.Create() })
                .ToList();

            int bytesRead = 0;
            byte[] buffer = new byte[1024];
            do
            {
                int count = buffer.Length - bytesRead;
                bytesRead = await message.Body.ReadAsync(buffer, 0, count, currentCacnellationToken);
                foreach (var outputMessage in handlersAndMessages.Select(x=>x.Message))
                    await outputMessage.Body.WriteAsync(buffer, 0, bytesRead, currentCacnellationToken);
            }while(bytesRead > 0);

            foreach (var tuple in handlersAndMessages)
            {
                tuple.Handler.HandleMessage(tuple.Message);
                tuple.Message.Body.Close();
            }
        }
    }
}
