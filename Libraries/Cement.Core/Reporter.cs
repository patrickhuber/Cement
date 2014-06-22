using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cement
{
    public class Reporter<T> : IObserver<T>      
        where T : class
    {
        private IDisposable unsubscriber;
        private Action<T> onNext;
        private Action onCompleted;
        private Action<Exception> onError;

        public Reporter(Action<T> onNext, Action onCompleted, Action<Exception> onError)
        {
            this.onNext = onNext;
            this.onCompleted = onCompleted;
            this.onError = onError;
        }

        public virtual void Observe(IObservable<T> provider)
        {
            unsubscriber = provider.Subscribe(this);
        }

        public virtual void Unsubscribe()
        {
            unsubscriber.Dispose();
        }

        public virtual void OnCompleted()
        {
            this.onCompleted();
        }

        public virtual void OnError(Exception error)
        {
            this.onError(error);
        }

        public virtual void OnNext(T value)
        {
            this.onNext(value);
        }
    }
}
