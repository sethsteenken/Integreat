using System;

namespace Integreat
{
    public interface IIntegrationArchiver
    {
        void Archive(Guid processId, string filePath);
    }
}
