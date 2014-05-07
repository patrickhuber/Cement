using System;
using System.IO;
using System.Threading.Tasks;
namespace Cement.Messages
{
    public interface IMessage : IDisposable
    {
        Stream Body { get; set; }
        IMessageContext Context { get; set; }
        void CopyTo(IMessage message);
        Task CopyToAsync(IMessage message);
    }
}
