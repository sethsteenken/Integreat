namespace Integreat.Core
{
    public interface IFile
    {
        string FileName { get; }
        string Directory { get; }
        string Extension { get; }
        string FullPath { get; }
        bool Exists { get; }
    }
}
