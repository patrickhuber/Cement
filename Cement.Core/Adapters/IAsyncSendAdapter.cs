using Cement.Messages;
using System;
using System.Threading.Tasks;
namespace Cement.Adapters
{
    public interface IAsyncSendAdapter
    {
        Task SendAsync(IMessage message);
    }
}
