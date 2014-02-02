using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;

namespace Integration.ServiceModel.Channels
{
    public class NullChannelFactory : ChannelFactoryBase<IOutputChannel>
    {
        public NullChannelFactory(NullBindingElement transportElement, BindingContext bindingContext)
            : base(bindingContext.Binding)
        { }

        protected override IOutputChannel OnCreateChannel(System.ServiceModel.EndpointAddress address, Uri via)
        {
            return new NullOutputChannel(this);
        }

        protected override IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
        {
            var onOpenDelegate = new Action<TimeSpan>(OnOpen);
            return onOpenDelegate.BeginInvoke(timeout, callback, state);
        }

        protected override void OnEndOpen(IAsyncResult result)
        {            
        }

        protected override void OnOpen(TimeSpan timeout)
        {
        }
    }
}
