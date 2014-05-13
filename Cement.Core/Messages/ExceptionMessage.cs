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
        {
            SetMessageBody(exception);
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

        private void SetMessageBody(Exception exception)
        {
            var exceptionBody = exception.ToString();
            var exceptionBytes = Encoding.UTF8.GetBytes(exceptionBody);

            this.Body = new MemoryStream(exceptionBytes);
        }


    }
}
