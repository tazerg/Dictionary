using System.Collections.Generic;
using System.Globalization;
using JHI.Dict.Model;
using Newtonsoft.Json;

namespace JHI.Dict.Serialization
{
    public class WordSerializer
    {
        private readonly JsonSerializerSettings _settings = new()
        {
            Culture = CultureInfo.InvariantCulture
        };

        public string Serialize(List<Word> words)
        {
            return JsonConvert.SerializeObject(words, _settings);
        }

        public List<Word> Deserialize(string json)
        {
            if (string.IsNullOrEmpty(json))
                return new List<Word>();
            
            return JsonConvert.DeserializeObject<List<Word>>(json, _settings);
        }
    }
}