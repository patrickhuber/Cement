using System;
using System.IO;
namespace Cement.Core.Tests.Integration.Threading.Tasks
{
    public interface IProcessor
    {
        void Process(Stream inStream, Stream outStream);
    }
}
