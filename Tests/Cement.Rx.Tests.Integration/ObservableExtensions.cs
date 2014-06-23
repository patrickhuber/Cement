using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Cement.Rx.Tests.Integration
{
    public static class ObservableExtensions
    {
        public static IObservable<byte[]> MinimumBuffer(this IObservable<byte[]> source, int bufferSize)
        {
            return Observable.Create<byte[]>(observer =>
            {
                var data = new List<byte>();

                return source.Subscribe(value =>
                {
                    data.AddRange(value);

                    if (data.Count > bufferSize)
                    {
                        observer.OnNext(data.ToArray());
                        data.Clear();
                    }
                },
                observer.OnError,
                () =>
                {
                    if (data.Count > 0)
                        observer.OnNext(data.ToArray());

                    observer.OnCompleted();
                });
            });            
        }

        public static IObservable<Unit> WriteToStream(this IObservable<byte[]> source, Stream stream)
        {
            var asyncWrite = Observable.FromAsyncPattern<
                byte[], int, int>(stream.BeginWrite, stream.EndWrite);

            return source.SelectMany(data =>
                asyncWrite(data, 0, data.Length)).Where(_ => false);
        }
    }
}
