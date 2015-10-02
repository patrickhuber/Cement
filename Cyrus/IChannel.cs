using System.Threading.Tasks;

namespace Cyrus
{
    public interface IChannel
    {
        void Send(IMessage message);
        Task SendAsync(IMessage message);
        IMessage Receive();
        Task<IMessage> ReceiveAsync();
    }
    
}
