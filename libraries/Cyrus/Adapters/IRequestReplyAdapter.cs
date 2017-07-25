using Cyrus.Channels;
using System.Threading.Tasks;

namespace Cyrus.Adapters
{
    /// <summary>
    /// Creates a correlated relationship between send and reply
    /// </summary>
    public interface IRequestReplyAdapter
    {

        IReceiveChannel RequestChannel { get; }

        ISendChannel ReplyChannel { get; }

        Task SendAsync();
    }
}
