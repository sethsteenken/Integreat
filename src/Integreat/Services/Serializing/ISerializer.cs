namespace Integreat
{
    public interface ISerializer
    {
        T DeserializeFile<T>(string filePath) where T : class;
    }
}
