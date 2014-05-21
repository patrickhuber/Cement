using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cement.Messages
{
    public class ExceptionMessage : Message, IMessage
    {
        public ExceptionMessage(Exception exception)
            : base(GetMessageBody(exception), new MessageContext())
        {            
            SetId();
            SetMessageType(exception);
        }

        private void SetMessageType(Exception exception)
        {
            this.Context.Attributes[MessageProperties.Type] = exception.GetType().ToString();
        }

        private void SetId()
        {
            this.Context.Attributes[MessageProperties.Id] = Guid.NewGuid().ToString();
        }

        private static Stream GetMessageBody(Exception exception)
        {
            var exceptionBody = exception.ToString();
            var exceptionBytes = Encoding.UTF8.GetBytes(exceptionBody);
            return new MemoryStream(exceptionBytes);
        }
    }
}
