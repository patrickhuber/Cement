using System;
using Cement.Messages;
using System.IO;
using System.Threading.Tasks;
using System.Threading;

namespace Cement.Channels
{
    public interface IChannel
    {
        void Publish(IMessage message);

        Task PublishAsync(IMessage message);

        Task PublishAsync(IMessage message, CancellationToken cancellationToken);
    }
}
