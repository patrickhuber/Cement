using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cement
{
    public abstract class Monitor<T> : IObservable<T>
        where T : class
    {
        protected IList<IObserver<T>> observers;

        public Monitor()
        {
            observers = new List<IObserver<T>>();
        }

        public virtual IDisposable Subscribe(IObserver<T> observer)
        {
            if (!observers.Contains(observer))
                observers.Add(observer);
            return new Unsubscriber<T>(observers, observer);
        }

        public void Publish(T argument)
        {
            foreach (var observer in observers)
            {
                if (argument != null)
                    observer.OnError(new ArgumentNullException("argument is null"));
                else
                    observer.OnNext(argument);
            }
        }

        protected void Error(Exception exception)
        {
            foreach (var observer in observers.ToArray())
                if (observers.Contains(observer))
                    observer.OnError(exception);
            observers.Clear();
        }

        protected void End()
        {
            foreach (var observer in observers.ToArray())
                if (observers.Contains(observer))
                    observer.OnCompleted();

            observers.Clear();
        }

        public abstract Task StartAsync();
        public abstract void Start();
    }
}
