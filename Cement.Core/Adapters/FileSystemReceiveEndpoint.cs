using Cement.Adapters;
using Cement.IO;
using Cement.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cement.Adapters
{
    public class FileSystemReceiveEndpoint : Monitor<IMessage>, IObserver<string>, IObservable<IMessage>
    {
        IDisposable unsubscriber;
        IFileSystem fileSystem;

        public FileSystemReceiveEndpoint(IObservable<string> observable, IFileSystem fileSystem)
        {
            unsubscriber = observable.Subscribe(this);
            this.fileSystem = fileSystem;
        }

        public void OnCompleted()
        {
            unsubscriber.Dispose();
        }

        public void OnError(Exception error)
        {
            Publish(new ExceptionMessage(error));
        }

        public void OnNext(string path)
        {
            IAdapterContext fileAdapterContext = CreateAdapterContext(path);
            IReceiveAdapter receiveAdapter = CreateAdapter(fileAdapterContext);
            IMessage message = receiveAdapter.Receive();
            Publish(message);
        }

        private IReceiveAdapter CreateAdapter(IAdapterContext fileChannelContext)
        {
            return new FileSystemReceiveAdapter(fileChannelContext, fileSystem);
        }

        private static IAdapterContext CreateAdapterContext(string path)
        {
            IAdapterContext fileAdapterContext = new AdapterContext();
            fileAdapterContext.Attributes.Add(Cement.Adapters.AdapterProperties.Uri, new Uri(path).AbsoluteUri);
            return fileAdapterContext;
        }

        public override Task StartAsync()
        {
            throw new NotImplementedException();
        }

        public override void Start()
        {
            throw new NotImplementedException();
        }
    }

}
