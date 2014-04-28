using System;
using System.Activities;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace Cement.Activities.Dataflow
{
    public class BufferPipeline<T> : TaskNativeActivity
    {
        private DataflowBlockOptions _options;
        public InArgument<IList<T>> Send { get; set; }
        public Collection<Activity> Children { get; set; }

        public BufferPipeline()
        {
            _options = new DataflowBlockOptions();
        }

        public BufferPipeline(DataflowBlockOptions options)
        {
            _options = options;
        }

        protected override Task ExecuteAsync(System.Activities.NativeActivityContext context)
        {
            throw new NotImplementedException();
        }
    }
}
