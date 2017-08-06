using System.Threading.Tasks;

namespace Cyrus
{
    /// <summary>
    /// Creates a correlated relationship between request and reply
    /// </summary>
    public interface IProcessor
    {

        IReceiveChannel RequestChannel { get; }

        ISendChannel ReplyChannel { get; }

        Task SendAsync();
    }
}
