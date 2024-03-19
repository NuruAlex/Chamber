using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chamber.Serialiation.InnerRetirver
{
    public class JsonRetriever : IRetriever
    {
        public string Extension => ".json";

        private readonly JsonSerializerSettings _serializeSettings = new()
        {
            TypeNameHandling = TypeNameHandling.Auto,
            NullValueHandling = NullValueHandling.Ignore,
        };

        public List<T> LoadFromFile<T>(string path) => JsonConvert.DeserializeObject<List<T>>(
                value: File.ReadAllText(path),
                settings: _serializeSettings) ?? [];

        public void SaveToFile<T>(List<T> data, string path) => File.WriteAllText(
                path: path,
                contents: JsonConvert.SerializeObject(data, _serializeSettings));
    }
}
