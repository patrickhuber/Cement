
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Cement.Msmq.Messages
{
    public class MsmqStream : Stream
    {
        bool _canRead;
        bool _canWrite;
        int _messageCount;
        Message _currentMessage;
        MessageQueue _messageQueue;
        string _correlationId;

        const int MaxMessageSize = 4193849;

        public MsmqStream(Message currentMessage, MessageQueue messageQueue)
        {
            _canRead = true;
            _canWrite = true;
            _currentMessage = currentMessage;
            _messageQueue = messageQueue;
            _messageCount = 0;
        }

        public override bool CanRead
        {
            get { return _canRead; }
        }

        public override bool CanSeek
        {
            get { return false; }
        }

        public override bool CanWrite
        {
            get { return _canWrite; }
        }

        public override void Flush()
        {
            _canWrite = false;
            _canRead = false;
            SendCurrentMessage();         
        }

        public override long Length
        {
            get { return _messageCount * MaxMessageSize + _currentMessage.BodyStream.Length; }
        }

        public override long Position
        {
            get
            {
                return _messageCount * MaxMessageSize + _currentMessage.BodyStream.Position;
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

        private int ReadCore(byte[] buffer, int offset, int count)
        {
            _canRead = true;
            _canWrite = false;
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

        private void WriteCore(byte[] buffer, int offset, int count)
        {
            _canRead = false;
            _canWrite = true;

            int bytesWritten = 0;
            while (bytesWritten < count)
            {
                int bytesRemaining = count - bytesWritten;
                int bufferBytesRemaining = MaxMessageSize - (int)_currentMessage.BodyStream.Position;
                if (bytesRemaining <= bufferBytesRemaining)
                {
                    _currentMessage.BodyStream.Write(buffer, offset, bytesRemaining);
                    bytesWritten += bytesRemaining;
                }
                else 
                {
                    _currentMessage.BodyStream.Write(buffer, offset, bufferBytesRemaining);
                    bytesWritten += bufferBytesRemaining;
                    SendCurrentMessage();
                }
            }
        }

        private void SendCurrentMessage()
        {
            if (_currentMessage.Id.StartsWith(Guid.Empty.ToString()))
            {
                _messageQueue.Send(_currentMessage);

                var newMessage = new Message();
                bool isFirstMessage = _messageCount == 0;
                if (isFirstMessage)
                {
                    _correlationId = _currentMessage.Id;
                }
                _currentMessage = newMessage;
                _currentMessage.CorrelationId = _correlationId;
                _messageCount++;
            }
        }        
    }
}
