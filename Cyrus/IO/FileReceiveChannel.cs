using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Cyrus.IO
{
    public class FileReceiveChannel : IReceiveChannel
    {
        public string Path { get; private set; }

        public string Filter { get; private set; }

        public IFileSystem FileSystem { get; private set; }

        public FileReceiveChannel(IFileSystem fileSystem, string path, string filter)
        {
            Path = path;
            Filter = filter;
            FileSystem = fileSystem;
        }

        /// <summary>
        /// Receives a file by creating opening the file for read and creating a new Message that encapsulates the stream.
        /// </summary>
        /// <returns>the message reader that contains the message.</returns>
        public IMessageReader Receive()
        {
            var file = GetFirstFile();
            if (file == null)
                return null;

            var fileStream = FileSystem.OpenFile(file, FileMode.Open);
            return new Message(fileStream, GetMessageHeaders(file));
        }

        private IDictionary<string, string> GetMessageHeaders(string file)
        {
            return new Dictionary<string, string>()
            {
                { FileReceiveProperties.Path, file },
                { FileReceiveProperties.Filter, Filter },
                { FileReceiveProperties.LastWriteTimeUtc, FileSystem.GetFileLastWriteTimeUtc(file).ToString() },
                { FileReceiveProperties.CreationTimeUtc, FileSystem.GetFileCreationTimeUtc(file).ToString() }
            };
        }

        /// <summary>
        /// Receives a message by writing the message to the given message writer.
        /// </summary>
        /// <param name="writer"></param>
        public void Receive(IMessageWriter writer)
        {
            var file = GetFirstFile();
            if (file == null)
                return;
            using (var fileStream = FileSystem.OpenFile(file, FileMode.Open))
            {
                Receive(writer, fileStream);
            }
        }

        private static void Receive(IMessageWriter writer, Stream stream)
        {
            byte[] buffer = new byte[1024];
            int bytesRead = 0;

            do
            {
                bytesRead = stream.Read(buffer, 0, buffer.Length);
                writer.Write(buffer, 0, bytesRead);
            } while (bytesRead > 0);
        }

        /// <summary>
        /// Given a message sink, the receive async method reads from the first file that matches the filter and path and writes the data to the message sink.
        /// </summary>
        /// <param name="writer">The message sink from which to write the data.</param>
        /// <returns>The message sink.</returns>
        public async Task ReceiveAsync(IMessageWriter writer)
        {
            var file = GetFirstFile();
            if (file == null)
                return;

            using (var fileStream = FileSystem.OpenFile(file, FileMode.Open))
            {
                await ReceiveAsync(writer, fileStream);
            }
        }

        private static async Task ReceiveAsync(IMessageWriter writer, Stream stream)
        {
            byte[] buffer = new byte[1024];
            int bytesRead = 0;

            do
            {
                bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                await writer.WriteAsync(buffer, 0, bytesRead);
            } while (bytesRead > 0);
        }

        private string GetFirstFile()
        {
            foreach (var file in FileSystem.EnumerateFiles(Path, Filter))
                return file;
            return null;
        }
    }
}