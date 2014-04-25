using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace Cement.Activities.Dataflow
{
    public class Buffer<T> : TaskActivity
    {
        private DataflowBlockOptions _options;
        public InArgument<IList<T>> Send { get; set; }
        public OutArgument<IList<T>> Receive { get; set; }

        public Buffer()
        {
            _options = new DataflowBlockOptions();
        }

        public Buffer(DataflowBlockOptions options)
        {
            _options = options;
        }

        protected async override Task ExecuteAsync(System.Activities.AsyncCodeActivityContext context)
        {
            var bufferBlock = new BufferBlock<T>(_options);
            var postList = Send.Get(context);
            var totalCount = postList.Count;

            foreach (var post in postList)
            {
                await bufferBlock.SendAsync(post);                
            }

            var receiveList = new List<T>();
            for (int i = 0; i < totalCount; i++)
            {
                receiveList.Add(await bufferBlock.ReceiveAsync());
            }

            Receive.Set(context, receiveList);
        }
    }
}
