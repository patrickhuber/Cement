using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;

namespace Referee.ServiceModel.Channels
{
    public class NullBindingElement : TransportBindingElement
    {
        public NullBindingElement() { }
        public NullBindingElement(NullBindingElement other) { }

        public override string Scheme
        {
            get { return "null"; }
        }

        public override BindingElement Clone()
        {
            return new NullBindingElement(this);
        }

        public override bool CanBuildChannelFactory<TChannel>(BindingContext context)
        {
            return typeof(TChannel) == typeof(IOutputChannel);
        }

        public override bool CanBuildChannelListener<TChannel>(BindingContext context)
        {
            return false;
        }

        public override IChannelFactory<TChannel> BuildChannelFactory<TChannel>(BindingContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");
            if (!CanBuildChannelFactory<TChannel>(context))
                throw new ArgumentException(String.Format("Unsupported channel type: {0}.", typeof(TChannel).Name));
            return (IChannelFactory<TChannel>)((object)new NullChannelFactory(this, context));
        }
    }
}
