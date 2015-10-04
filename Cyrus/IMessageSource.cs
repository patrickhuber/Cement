namespace Cyrus
{
    public interface IMessageSource
    {
        IMessageHeader MessageHeader { get; }
        int Read(byte[] data, int offset, int length);
    }
}