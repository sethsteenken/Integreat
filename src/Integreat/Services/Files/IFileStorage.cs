﻿using System.Collections.Generic;

namespace Integreat
{
    public interface IFileStorage
    {
        IFile GetFile(string path);
        IReadOnlyList<IFile> GetFiles(string directory, string filter = null);
        bool Exists(string path);
        string Read(string path);
        void Write(string filePath, string content);
        void Delete(string path);
        void CreateDirectory(string path);
        void WaitForFileReady(string filePath, int timeoutInSeconds, int interval = 500);
        string CopyFile(string fromFilePath, string toFilePath);
        string Extract(string filePath, string outputDirectory);
    }
}
