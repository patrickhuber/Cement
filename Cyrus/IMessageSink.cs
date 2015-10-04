namespace Cyrus
{
    public interface IMessageSink
    {
        IMessageHeader Header { get; }
        void Write(byte[] data, int offset, int length);
    }
}
