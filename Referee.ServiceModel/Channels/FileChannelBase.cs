using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;

namespace Referee.ServiceModel.Channels
{
    public abstract class FileChannelBase : ChannelBase, IChannel
    {
        protected FileChannelBase(ChannelManagerBase channelManager)
            : base(channelManager)
        { }

        protected override void OnAbort()
        {
            throw new NotImplementedException();
        }

        protected override IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
        {
            throw new NotImplementedException();
        }

        protected override IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
        {
            throw new NotImplementedException();
        }

        protected override void OnClose(TimeSpan timeout)
        {
            throw new NotImplementedException();
        }

        protected override void OnEndClose(IAsyncResult result)
        {
            throw new NotImplementedException();
        }

        protected override void OnEndOpen(IAsyncResult result)
        {
            throw new NotImplementedException();
        }

        protected override void OnOpen(TimeSpan timeout)
        {
            throw new NotImplementedException();
        }
    }
}
