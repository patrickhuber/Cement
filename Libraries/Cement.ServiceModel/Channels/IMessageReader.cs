using System;
using System.ServiceModel.Channels;

namespace Cement.ServiceModel.Channels
{
    public interface IMessageReader
    {
        Message ReadBuffered();
        Message ReadStreamed();
    }
}
