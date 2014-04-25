using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cement.Threading.Tasks;

namespace Cement.Activities.Dataflow
{
    public abstract class TaskNativeActivity<T> : AsyncNativeActivity<T>
    {
        protected abstract Task<T> ExecuteAsync(NativeActivityContext context);

        protected override IAsyncResult BeginExecute(System.Activities.NativeActivityContext context, AsyncCallback callback, object state)
        {
            var executeTask = ExecuteAsync(context);
            return executeTask.AsApm(callback, state);
        }

        protected override T EndExecute(System.Activities.NativeActivityContext context, IAsyncResult result)
        {
            return ((Task<T>)result).Result;
        }
    }
}
