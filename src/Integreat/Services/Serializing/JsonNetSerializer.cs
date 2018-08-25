using Newtonsoft.Json;

namespace Integreat
{
    public class JsonNetSerializer : ISerializer
    {
        private readonly IFileStorage _fileStorage;

        static JsonNetSerializer()
        {
            JsonConvert.DefaultSettings = () => Settings;
        }

        public JsonNetSerializer(IFileStorage fileStorage)
        {
            _fileStorage = fileStorage;
        }

        public static JsonSerializerSettings Settings { get; set; } = new JsonSerializerSettings()
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            DateTimeZoneHandling = DateTimeZoneHandling.Utc
        };

        public T DeserializeFile<T>(string filePath) where T : class
        {
            Guard.IsNotNull(filePath, nameof(filePath));
            return Deserialize<T>(_fileStorage.Read(filePath));
        }

        private T Deserialize<T>(string serializedValue) where T : class
        {
            if (string.IsNullOrWhiteSpace(serializedValue))
                return default(T);

            return JsonConvert.DeserializeObject<T>(serializedValue);
        }
    }
}
