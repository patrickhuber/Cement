using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cement.Msmq.Messages
{
    public class MsmqStream : Stream
    {
        bool _canRead;
        bool _canWrite;
        long _position;

        const int MaxMessageSize = 4193849;

        public override bool CanRead
        {
            get { throw new NotImplementedException(); }
        }

        public override bool CanSeek
        {
            get { return false; }
        }

        public override bool CanWrite
        {
            get { throw new NotImplementedException(); }
        }

        public override void Flush()
        {
            throw new NotImplementedException();
        }

        public override long Length
        {
            get { throw new NotImplementedException(); }
        }

        public override long Position
        {
            get
            {
                return _position;
            }
            set
            {
                Seek(value, SeekOrigin.Begin);
            }
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            if (!CanRead)
                throw new InvalidOperationException("Unable to read because the stream only supports one way operations.");
            return 0;
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            if (!CanSeek)
                throw new InvalidOperationException("Unable to seek because the stream does not support seeking.");
            throw new NotImplementedException("This method is not implemented, because we never thought you'd get this far.");
        }

        public override void SetLength(long value)
        {
            throw new NotImplementedException();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            if (!CanWrite)
                throw new InvalidOperationException("Unable to write because the stream only supports one way operations.");
        }
    }
}
