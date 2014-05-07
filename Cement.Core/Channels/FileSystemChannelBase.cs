using Cement.IO;
using Cement.Messages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cement.Channels
{
    public abstract class FileSystemChannelBase : IChannel
    {
        protected IChannelContext channelContext;
        protected IFileSystem fileSystem;

        protected FileSystemChannelBase(IChannelContext channelContext, IFileSystem fileSystem)
        {
            this.channelContext = channelContext;
            this.fileSystem = fileSystem;
        }
        
        protected Uri GetChannelUri()
        {
            string uriText;
            Uri uri;
            if (!channelContext.Attributes.TryGetValue(Cement.Channels.ChannelProperties.Uri, out uriText))
                throw new InvalidDataException("Unable to locate uri, the channel is misconfigured. Please specify a Uri in the Channel Settings.");
            uri = default(Uri);
            try
            {
                uri = new Uri(uriText);
            }
            catch (FormatException ex)
            {
                throw new FormatException("Channel Uri is not in the correct format. Please format as a System.Uri object.", ex);
            }
            return uri;
        }

        public string Protocol
        {
            get { return FileSystemProperties.Protocol; }
        }
    }
}
