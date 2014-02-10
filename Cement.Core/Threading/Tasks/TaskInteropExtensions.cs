using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cement.Threading.Tasks
{
    public static class TaskInteropExtensions
    {
        /// <summary>
        /// <see cref="http://blogs.msdn.com/b/pfxteam/archive/2011/06/27/using-tasks-to-implement-the-apm-pattern.aspx"/>
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="task"></param>
        /// <param name="callback"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public static IAsyncResult AsApm<T>(
            this Task<T> task,
            AsyncCallback callback,
            object state)
        {
            if (task == null) throw new ArgumentNullException("task");
            var tcs = new TaskCompletionSource<T>(state);
            task.ContinueWith(t =>
            {
                if (t.IsFaulted)
                    tcs.TrySetException(t.Exception.InnerExceptions);
                else if (t.IsCanceled)
                    tcs.TrySetCanceled();
                else
                    tcs.TrySetResult(t.Result);

                if (callback != null)
                    callback(tcs.Task);
            },
            TaskScheduler.Default);
            return tcs.Task;
        }

        public static IAsyncResult AsApm(
            this Task task,
            AsyncCallback callback,
            object state)
        {
            if (task == null) throw new ArgumentNullException("task");
            var tcs = new TaskCompletionSource<bool>(state);
            task.ContinueWith(t =>
            {
                if (t.IsFaulted)
                    tcs.TrySetException(t.Exception.InnerExceptions);
                else if (t.IsCanceled)
                    tcs.TrySetCanceled();
                else
                    tcs.TrySetResult(false);

                if (callback != null)
                    callback(tcs.Task);
            },
            TaskScheduler.Default);

            return tcs.Task;
        }

        /// <summary>
        /// Task.Delay is only available in 4.5
        // for .NET 4.0 use TaskExtensions.Delay (rename of course)            
        /// </summary>
        /// <param name="millisecondsTimeout"></param>
        /// <returns></returns>
        public static Task<DateTimeOffset> Delay(int millisecondsTimeout)
        {
            var tcs = new TaskCompletionSource<DateTimeOffset>();
            new Timer(self =>
            {
                ((IDisposable)self).Dispose();
                tcs.TrySetResult(DateTimeOffset.UtcNow);
            }).Change(millisecondsTimeout, -1);
            return tcs.Task;
        }
    }
}
