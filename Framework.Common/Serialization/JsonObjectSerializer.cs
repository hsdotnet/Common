using Newtonsoft.Json;

namespace Framework.Common.Serialization
{
    public class JsonObjectSerializer : IObjectSerializer
    {
        public T DeserializeObject<T>(string value)
        {
            return JsonConvert.DeserializeObject<T>(value);
        }

        public string SerializeObject<T>(T t)
        {
            return JsonConvert.SerializeObject(t);
        }
    }
}