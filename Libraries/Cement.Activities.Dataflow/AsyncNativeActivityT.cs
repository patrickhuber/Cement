using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cement.Activities.Dataflow
{
    /// <summary>
    /// <see cref="http://blogs.msdn.com/b/tilovell/archive/2011/06/09/wf4-they-have-asynccodeactivity-why-not-asyncnativeactivity.aspx"/>
    /// </summary>
    public abstract class AsyncNativeActivity<T> : NativeActivity<T>
    {
        private Variable<NoPersistHandle> NoPersistHandle { get; set; }
        private Variable<Bookmark> Bookmark { get; set; }

        protected override bool CanInduceIdle
        {
            get
            {
                return true; // we create bookmarks
            }
        }

        protected override void Execute(NativeActivityContext context)
        {
            var noPersistHandle = NoPersistHandle.Get(context);
            noPersistHandle.Enter(context);
            
            var bookmark = context.CreateBookmark(BookmarkResumptionCallback);
            var bookmarkResumptionHelper = context.GetExtension<BookmarkResumptionHelper>();
            
            Action<IAsyncResult> resumeBookmarkAction = (result) =>
            {
                bookmarkResumptionHelper.ResumeBookmark(bookmark, result);
            };
            
            IAsyncResult asyncResult = BeginExecute(context, AsyncCompletionCallback, resumeBookmarkAction);
            if (asyncResult.CompletedSynchronously)
            {
                noPersistHandle.Exit(context);
                context.RemoveBookmark(bookmark);
                var result = EndExecute(context, asyncResult);
                Result.Set(context, result);
            }
        }

        private void AsyncCompletionCallback(IAsyncResult asyncResult)
        {
            if (!asyncResult.CompletedSynchronously)
            {
                Action<IAsyncResult> resumeBookmark = asyncResult.AsyncState as Action<IAsyncResult>;
                resumeBookmark.Invoke(asyncResult);
            }
        }

        protected abstract T EndExecute(NativeActivityContext context, IAsyncResult asyncResult);
        protected abstract IAsyncResult BeginExecute(NativeActivityContext context, AsyncCallback callback, object state);

        private void BookmarkResumptionCallback(NativeActivityContext context, System.Activities.Bookmark bookmark, object value)
        {
            var noPersistHandle = NoPersistHandle.Get(context);
            noPersistHandle.Exit(context);
            // unnecessary since it's not multiple resume:
            // context.RemoveBookmark(bookmark);

            IAsyncResult asyncResult = value as IAsyncResult;
            var result = this.EndExecute(context, asyncResult);
            this.Result.Set(context, result);
        }

        protected override void CacheMetadata(NativeActivityMetadata metadata)
        {
            this.NoPersistHandle = new Variable<NoPersistHandle>();
            this.Bookmark = new Variable<Bookmark>();
            metadata.AddImplementationVariable(this.NoPersistHandle);
            metadata.AddImplementationVariable(this.Bookmark);
            metadata.RequireExtension<BookmarkResumptionHelper>();
            metadata.AddDefaultExtensionProvider<BookmarkResumptionHelper>(() => new BookmarkResumptionHelper());
        }
    }    
}
