using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cement.IO
{
    /// <summary>
    /// Provides a write only stream that multiple casts data do downstream Stream objects.
    /// </summary>
    public class MultiCastStream : Stream
    {
        private IList<Stream> streamList;
        public MultiCastStream(IEnumerable<Stream> streams)
            : base()
        {
            streamList = new List<Stream>(streams);
        }

        public override bool CanRead { get { return false; } }

        public override bool CanSeek { get { return false; } }

        public override bool CanWrite { get { return true; } }

        public override void Flush()
        {
            foreach (var stream in streamList)
                stream.Flush();
        }

        public override async Task FlushAsync(System.Threading.CancellationToken cancellationToken)
        {
            foreach (var stream in streamList)
                await stream.FlushAsync(cancellationToken);
        }

        public override long Length
        {
            get { throw new NotSupportedException(); }
        }

        public override long Position
        {
            get { throw new NotSupportedException(); }
            set { throw new NotSupportedException(); }
        }
        
        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotSupportedException();
        }

        public override void SetLength(long value)
        {
            throw new NotSupportedException();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            foreach (var stream in streamList)
                stream.Write(buffer, offset, count);
        }

        public override async Task WriteAsync(byte[] buffer, int offset, int count, System.Threading.CancellationToken cancellationToken)
        {
            foreach (var stream in streamList)
                await stream.WriteAsync(buffer, offset, count, cancellationToken);
        }
    }
}
