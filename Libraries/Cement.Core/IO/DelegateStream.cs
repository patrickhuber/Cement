using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cement.IO
{
    public class DelegateStream : Stream
    {
        Action<byte[], int, int> onWrite;
        Func<byte[], int, int, int> onRead;
        
        public DelegateStream(
            Func<byte[], int, int, int> onRead, 
            Action<byte[], int, int> onWrite)
        {
            this.onWrite = onWrite;
            this.onRead = onRead;
        }

        public override bool CanRead
        {
            get {return true; }
        }

        public override bool CanSeek
        {
            get { return false; }
        }

        public override bool CanWrite
        {
            get { return true; }
        }

        public override void Flush()
        {
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
            return onRead(buffer, offset, count);
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
            onWrite(buffer, offset, count);
        }
    }
}
