using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cement.IO
{
    /// <summary>
    /// <see cref="http://msdn.microsoft.com/en-us/magazine/cc163290.aspx">msdn article on pipes and filters</see>
    /// </summary>
    public class BlockingStream : Stream
    {
        private Queue<byte> _byteQueue;

        public BlockingStream()
            : base()
        {
            _byteQueue = new Queue<byte>();
        }

        public override bool CanRead
        {
            get { throw new NotImplementedException(); }
        }

        public override bool CanSeek
        {
            get { throw new NotImplementedException(); }
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
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            throw new NotImplementedException();
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotImplementedException();
        }

        public override void SetLength(long value)
        {
            throw new NotImplementedException();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotImplementedException();
        }
    }
}
