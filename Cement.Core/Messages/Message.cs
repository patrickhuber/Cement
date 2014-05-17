using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cement.Messages
{
    public class Message : IMessage
    {
        public Message(Stream body, IMessageContext context)
        {
            Body = body;
            Context = context;
        }

        public System.IO.Stream Body { get; protected set; }

        public IMessageContext Context { get; protected set; }

        public void Dispose()
        {
            Body.Dispose();
        }

        public void CopyTo(IMessage message)
        {
            Body.CopyTo(message.Body);
            Context.CopyTo(message.Context);
        }

        public async Task CopyToAsync(IMessage message)
        {
            var bodyTask = Body.CopyToAsync(message.Body);
            Context.CopyTo(message.Context);
            await bodyTask;
        }
    }
}
