using Cement.Adapters;
using Cement.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cement.Channels
{
    public class PassThroughChannel : IObservable<IMessage>, IObserver<IMessage>
    {
        protected Monitor<IMessage> messageMonitor;

        public PassThroughChannel() { }

        public void Send(IMessage message) { }

        public IDisposable Subscribe(IObserver<IMessage> observer)
        {
            return messageMonitor.Subscribe(observer);
        }

        public void OnCompleted()
        {
            // clean up resources
        }

        public void OnError(Exception error)
        {
            // log errors
        }

        public void OnNext(IMessage value)
        {
            // process?
        }
    }
}
