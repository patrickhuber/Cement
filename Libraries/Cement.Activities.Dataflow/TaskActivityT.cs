using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cement.Threading.Tasks;
namespace Cement.Activities.Dataflow
{
    public abstract class TaskActivity<T> : System.Activities.AsyncCodeActivity<T>
    {
        protected abstract Task<T> ExecuteAsync(AsyncCodeActivityContext context);

        protected override IAsyncResult BeginExecute(System.Activities.AsyncCodeActivityContext context, AsyncCallback callback, object state)
        {
            var executeTask = ExecuteAsync(context);
            return executeTask.AsApm(callback, state);
        }

        protected override T EndExecute(System.Activities.AsyncCodeActivityContext context, IAsyncResult result)
        {
            return ((Task<T>)result).Result;
        }
    }
}
