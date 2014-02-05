using Cement.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;

namespace Cement.ServiceModel.Channels
{
    public abstract class FileChannelBase : ChannelBase, IChannel
    {
        protected readonly IMessageEncoder messageEncoder;
        protected readonly IBufferManager bufferManager;
        protected readonly IFileSystem fileSystem;

        protected FileChannelBase(
            ChannelManagerBase channelManager,
            IBufferManager bufferManager, 
            IMessageEncoderFactory encoderFactory, 
            IFileSystem fileSystem)
            : base(channelManager)
        {
            this.bufferManager = bufferManager;
            this.messageEncoder = encoderFactory.CreateSessionEncoder();
        }
                
        protected override void OnAbort()
        {
        }

        protected override IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
        {
            Action<TimeSpan> onCloseDelegate = new Action<TimeSpan>(OnClose);
            return onCloseDelegate.BeginInvoke(timeout, callback, state);
        }

        protected override IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
        {
            Action<TimeSpan> onOpenDelegate = new Action<TimeSpan>(OnOpen);
            return onOpenDelegate.BeginInvoke(timeout, callback, state);
        }

        protected override void OnClose(TimeSpan timeout)
        {            
        }

        protected override void OnEndClose(IAsyncResult result)
        {            
        }

        protected override void OnEndOpen(IAsyncResult result)
        {            
        }

        protected override void OnOpen(TimeSpan timeout)
        {            
        }

        protected PathAndPattern ParseFileUri(Uri uri)
        {
            PathAndPattern pathAndPattern = new PathAndPattern();
            if (!uri.IsFile)
                throw new IOException(
                    string.Format("The path {0} is not a file path.", uri.LocalPath));
            pathAndPattern.Pattern = Path.GetFileName(uri.LocalPath);
            pathAndPattern.Path = Path.GetDirectoryName(uri.LocalPath);
            return pathAndPattern;
        }

        protected class PathAndPattern
        {
            public string Path { get; set; }
            public string Pattern { get; set; }
        }
    }
}
