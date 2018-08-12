using Integreat.Internal;
using System.IO;

namespace Integreat
{
    public sealed class File : IFile
    {
        public File(string fullPath)
            : this (new FileInfo(fullPath))
        {
            
        }

        internal File(FileInfo fileInfo)
        {
            Guard.IsNotNull(fileInfo, nameof(fileInfo));

            FileName = fileInfo.Name;
            Directory = fileInfo.DirectoryName;
            Extension = fileInfo.Extension?.Replace(".", "").ToLower();
            FullPath = fileInfo.FullName;
            Exists = fileInfo.Exists;
        }

        public string FileName { get; private set; }
        public string Directory { get; private set; }
        public string Extension { get; private set; }
        public string FullPath { get; private set; }
        public bool Exists { get; private set; }

        public override string ToString()
        {
            return FullPath;
        }
    }
}
