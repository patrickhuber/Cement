using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace Cement.ServiceModel.Channels
{
    public class MessageReader : Cement.ServiceModel.Channels.IMessageReader
    {
        const int MaxBufferSize = 64 * 1024;
        const int MaxSizeOfHeaders = 4 * 1024;

        private Stream stream;
        private BufferManager bufferManager;
        private IMessageEncoder messageEncoder;
        private long maxReceivedMessageSize;

        public MessageReader(
            Stream stream, 
            BufferManager bufferManager, 
            IMessageEncoder messageEncoder, 
            long maxReceivedMessageSize)
        {
            this.stream = stream;
            this.bufferManager = bufferManager;
            this.messageEncoder = messageEncoder;
            this.maxReceivedMessageSize = maxReceivedMessageSize;
        }

        public MessageReader(Stream stream, BufferManager bufferManager, IMessageEncoderFactory messageEncoderFactory, long maxReceivedMessageSize)
            : this(stream, bufferManager, messageEncoderFactory.CreateSessionEncoder(), maxReceivedMessageSize)
        { }

        public Message ReadStreamed()
        {
            long totalBytes;
            try
            {
                totalBytes = stream.Length;
                AssertSizeOfStreamIsNotTooLargeForBuffer(totalBytes);
                AssertMessageBelowMaximumSize(totalBytes);

                return this.messageEncoder.ReadMessage(stream, MaxSizeOfHeaders);
            }
            catch (Exception e)
            {
                throw ConvertException(e);
            }
        }

        public Message ReadBuffered()
        {
            byte[] buffer;
            long totalBytes;
            try
            {
                totalBytes = stream.Length;

                AssertSizeOfStreamIsNotTooLargeForBuffer(totalBytes);
                AssertMessageBelowMaximumSize(totalBytes);
                
                buffer = CreateBuffer(totalBytes);
                Message message = ReadMessage(stream, buffer, totalBytes);
                ReclaimBuffer(buffer);
                
                return message;
            }
            catch (Exception e)
            {
                throw ConvertException(e);
            }
        }

        private void AssertSizeOfStreamIsNotTooLargeForBuffer(long totalBytes)
        {
            if(IsStreamTooLarge(totalBytes))
                throw new CommunicationException(
                    string.Format("Message exceeds maximum size: {0} > {1}", totalBytes, maxReceivedMessageSize));
        }

        private static bool IsStreamTooLarge(long totalBytes)
        {
            return totalBytes > int.MaxValue;
        }

        private void AssertMessageBelowMaximumSize(long totalBytes)
        { 
            if(IsMessageAboveMaximumSize(totalBytes))
                throw new CommunicationException(String.Format("Message exceeds maximum size: {0} > {1}.", totalBytes, maxReceivedMessageSize));
        }

        private bool IsMessageAboveMaximumSize(long totalBytes)
        {
            return totalBytes > maxReceivedMessageSize;
        }

        private byte[] CreateBuffer(long totalBytes)
        {
            return bufferManager.TakeBuffer((int)totalBytes);
        }

        private Message ReadMessage(Stream stream, byte[] buffer, long totalBytes)
        {
            int bytesRead = 0;
            while (bytesRead < totalBytes)
            {
                int count = stream.Read(buffer, bytesRead, (int)totalBytes - bytesRead);
                if (count == 0)
                {
                    throw new CommunicationException(String.Format("Unexpected end of message after {0} of {1} bytes.", bytesRead, totalBytes));
                }
                bytesRead += count;
            }
            ArraySegment<byte> arraySegment = new ArraySegment<byte>(buffer, 0, (int)totalBytes);
            return messageEncoder.ReadMessage(arraySegment, this.bufferManager);
        }

        private void ReclaimBuffer(byte[] buffer)
        {
            bufferManager.ReturnBuffer(buffer);
        }

        protected static Exception ConvertException(Exception exception)
        {
            Type exceptionType = exception.GetType();
            if (exceptionType == typeof(System.IO.DirectoryNotFoundException) ||
                exceptionType == typeof(System.IO.FileNotFoundException) ||
                exceptionType == typeof(System.IO.PathTooLongException))
            {
                return new EndpointNotFoundException(exception.Message, exception);
            }
            return new CommunicationException(exception.Message, exception);
        }
    }
}
