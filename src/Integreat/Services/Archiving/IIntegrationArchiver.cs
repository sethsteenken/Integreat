using System;

namespace Integreat
{
    public interface IIntegrationArchiver
    {
        void Archive(string filePath, Guid processId);
    }
}
