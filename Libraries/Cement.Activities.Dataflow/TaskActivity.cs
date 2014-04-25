using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cement.Threading.Tasks;

namespace Cement.Activities.Dataflow
{
    public abstract class TaskActivity : System.Activities.AsyncCodeActivity
    {
        protected abstract Task ExecuteAsync(AsyncCodeActivityContext context);

        protected override IAsyncResult BeginExecute(System.Activities.AsyncCodeActivityContext context, AsyncCallback callback, object state)
        {
            var executeTask = ExecuteAsync(context);
            return executeTask.AsApm(callback, state);
        }

        protected override void EndExecute(System.Activities.AsyncCodeActivityContext context, IAsyncResult result)
        {
            ((Task)result).Wait();
        }
    }
}
