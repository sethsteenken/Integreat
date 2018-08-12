using Integreat.Core.Internal;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading;

namespace Integreat.Core
{
    public class SystemIOFileStorage : IFileStorage
    {
        public string CopyFile(string fromFilePath, string toFilePath)
        {
            CreateDirectory(toFilePath);
            System.IO.File.Copy(fromFilePath, toFilePath);
            return toFilePath;
        }

        public void CreateDirectory(string path)
        {
            string directory = Path.HasExtension(path) ? Path.GetDirectoryName(path) : path;
            Directory.CreateDirectory(directory);
        }

        public void Delete(string path)
        {
            if (Path.HasExtension(path))
                System.IO.File.Delete(path);
            else
                Directory.Delete(path, recursive: true);
        }

        public bool Exists(string path)
        {
            return Path.HasExtension(path) ? new File(path).Exists : Directory.Exists(path);
        }

        public string Extract(string filePath, string outputDirectory)
        {
            ZipFile.ExtractToDirectory(filePath, outputDirectory);
            return outputDirectory;
        }

        public IFile GetFile(string path)
        {
            Guard.IsNotNull(path, nameof(path));
            return new File(path);
        }

        public IReadOnlyList<IFile> GetFiles(string directory, string filter = null)
        {
            filter = string.IsNullOrWhiteSpace(filter) ? "*" : filter;

            return Directory.GetFiles(directory, filter, SearchOption.AllDirectories)
                        .Select(x => new FileInfo(x))
                        .OrderByDescending(fileInfo => fileInfo.LastWriteTimeUtc)
                        .Select(fileInfo => new File(fileInfo))
                        .ToList();
        }

        public string Read(string path)
        {
            return System.IO.File.ReadAllText(path);
        }

        public void WaitForFileReady(string filePath, int timeoutInSeconds, int interval = 500)
        {
            bool fileReady = false;
            timeoutInSeconds = timeoutInSeconds * 1000; // convert to milliseconds
            var time = Stopwatch.StartNew();
            interval = interval < 100 ? 100 : interval;

            while (time.ElapsedMilliseconds < timeoutInSeconds && !fileReady)
            {
                fileReady = FileIsReady(filePath);
                if (!fileReady)
                    Thread.Sleep(interval);
            }

            if (!fileReady)
                throw new FileLoadException($"File '{filePath}' not ready within timeout ({timeoutInSeconds} milliseconds).");
        }

        private static bool FileIsReady(string filePath)
        {
            // If the file can be opened for exclusive access it means that the file
            // is no longer locked by another process.
            try
            {
                using (FileStream inputStream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    return inputStream.Length > 0;
                }
            }
            catch
            {
                return false;
            }
        }

        public void Write(string filePath, string content)
        {
            System.IO.File.WriteAllText(filePath, content);
        }
    }
}
