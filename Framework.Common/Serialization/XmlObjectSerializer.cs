using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Framework.Common.Serialization
{
    public class XmlObjectSerializer : IObjectSerializer
    {
        public T DeserializeObject<T>(string value)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));

            using (StreamReader sr = new StreamReader(new MemoryStream(Encoding.UTF8.GetBytes(value)), Encoding.UTF8))
            {
                return (T)xmlSerializer.Deserialize(sr);
            }
        }

        public string SerializeObject<T>(T t)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));

            using (MemoryStream ms = new MemoryStream())
            {
                using (XmlTextWriter writer = new XmlTextWriter(ms, Encoding.UTF8))
                {
                    xmlSerializer.Serialize(writer, t);

                    return Encoding.UTF8.GetString(ms.ToArray());
                }
            }
        }
    }
}