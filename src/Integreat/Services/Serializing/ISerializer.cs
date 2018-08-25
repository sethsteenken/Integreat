namespace Integreat
{
    public interface ISerializer
    {
        T Deserialize<T>(string serializedValue) where T : class;
    }
}
