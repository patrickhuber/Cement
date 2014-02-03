using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;

namespace Cement.ServiceModel.Channels
{
    public class NullOutputChannel : ChannelBase, IChannel, IOutputChannel
    {
        public NullOutputChannel(ChannelManagerBase channelManager)
            : base(channelManager)
        {  
        }

        #region ChannelBase
        protected override void OnAbort()
        {
        }

        protected override IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
        {
            var onCloseDelegate = new Action<TimeSpan>(OnClose);
            return onCloseDelegate.BeginInvoke(timeout, callback, state);
        }

        protected override IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
        {
            var onOpenDelegate = new Action<TimeSpan>(OnOpen);
            return onOpenDelegate.BeginInvoke(timeout, callback, state);
        }

        protected override void OnClose(TimeSpan timeout)
        {
        }

        protected override void OnEndClose(IAsyncResult result)
        {
        }

        protected override void OnEndOpen(IAsyncResult result)
        {
        }

        protected override void OnOpen(TimeSpan timeout)
        {
        }

        #endregion ChannelBase

        #region IOutputChannel

        public IAsyncResult BeginSend(Message message, TimeSpan timeout, AsyncCallback callback, object state)
        {
            var sendDelegate = new Action<Message, TimeSpan>(Send);
            return sendDelegate.BeginInvoke(message, timeout, callback, state);            
        }

        public IAsyncResult BeginSend(Message message, AsyncCallback callback, object state)
        {
            var sendDelegate = new Action<Message>(Send);
            return sendDelegate.BeginInvoke(message, callback, state);   
        }

        public void EndSend(IAsyncResult result)
        {
        }

        public System.ServiceModel.EndpointAddress RemoteAddress
        {
            get { return (EndpointAddress)null; }
        }

        public void Send(Message message, TimeSpan timeout)
        {
        }

        public void Send(Message message)
        {            
        }

        public Uri Via
        {
            get;
            private set;
        }

        #endregion IOutputChannel
    }
}
