using Cement.Adapters;
using Cement.Messages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cement.Channels
{
    public class PassThroughChannel : IChannel
    {
        public PassThroughChannel()
        {

        }

        public IMessage CreateMessage(IMessageContext messageContext)
        {
            return null;
        }


        public void Publish(IMessage message)
        {
            throw new NotImplementedException();
        }
    }

    public class MonitorStream : Stream
    {
        private Monitor<ArraySegment<byte>> monitor;

        public MonitorStream(Monitor<ArraySegment<byte>> monitor)
        {
            this.monitor = monitor;
        }

        public override bool CanRead { get { return false; } }

        public override bool CanSeek { get { return false; } }

        public override bool CanWrite { get { return true; } }

        public override void Flush()
        {
            throw new NotSupportedException();
        }

        public override long Length
        {
            get { throw new NotSupportedException(); }
        }

        public override long Position
        {
            get
            {
                throw new NotSupportedException();
            }
            set
            {
                throw new NotSupportedException();
            }
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException();
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotSupportedException();
        }

        public override void SetLength(long value)
        {
            throw new NotSupportedException();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            monitor.Publish(new ArraySegment<byte>(buffer, offset, count));
        }
    }
}
