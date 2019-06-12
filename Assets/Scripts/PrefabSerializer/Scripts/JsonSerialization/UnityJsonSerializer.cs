using System.Collections.Generic;
using Newtonsoft.Json;
using PrefabSerializer.Scripts.JsonSerialization.Converters;

namespace PrefabSerializer.Scripts.JsonSerialization
{
    public class UnityJsonSerializer : IJsonSerializer
    {
        private readonly JsonSerializerSettings _serializerSettings;


        public UnityJsonSerializer()
        {
            _serializerSettings = new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                ContractResolver = new ComponentContractResolver(),
                Converters = new List<JsonConverter>()
                {
                    new GameObjectConverter(),
                    new TransformConverter(),
                    new DefaultComponentConverter(),
                },
            };
        }


        public string Serialize(object value, bool compressJson = true)
        {
            var formatting = compressJson ? Formatting.None : Formatting.Indented;
            return JsonConvert.SerializeObject(value, formatting, _serializerSettings);
        }

        public TValue Deserialize<TValue>(string jsonValue)
        {
            return JsonConvert.DeserializeObject<TValue>(jsonValue, _serializerSettings);
        }
    }
}