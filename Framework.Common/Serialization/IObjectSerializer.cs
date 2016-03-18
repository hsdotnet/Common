namespace Framework.Common.Serialization
{
    public interface IObjectSerializer
    {
        string SerializeObject<T>(T t);

        T DeserializeObject<T>(string value);
    }
}