using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace PrefabSerializer.Scripts.Extensions
{
    public static class JsonConverterExtensions
    {
        public static void SerializeOnOtherConverter(this JsonConverter source, JsonWriter writer, object value,
            JsonSerializer jsonSerializer, IContractResolver newContractResolver = null)
        {
            var isNeedToRestoreConverter = jsonSerializer.Converters.Any(c => c == source);
            if (isNeedToRestoreConverter)
                jsonSerializer.Converters.Remove(source);

            var oldContractResolver = jsonSerializer.ContractResolver;
            if (newContractResolver != null)
                jsonSerializer.ContractResolver = newContractResolver;

            try
            {
                jsonSerializer.Serialize(writer, value, value.GetType());
            }
            finally
            {
                if (isNeedToRestoreConverter)
                    jsonSerializer.Converters.Add(source);
                if (newContractResolver != null)
                    jsonSerializer.ContractResolver = oldContractResolver;
            }
        }

        public static TEntity DeserializeOnOtherConverter<TEntity>(this JsonConverter source, JsonReader reader,
            JsonSerializer jsonSerializer, IContractResolver newContractResolver = null)
        {
            var isNeedToRestoreConverter = jsonSerializer.Converters.Any(c => c == source);
            if (isNeedToRestoreConverter)
                jsonSerializer.Converters.Remove(source);

            var oldContractResolver = jsonSerializer.ContractResolver;
            if (newContractResolver != null)
                jsonSerializer.ContractResolver = newContractResolver;

            try
            {
                var entity = jsonSerializer.Deserialize<TEntity>(reader);
                return entity;
            }
            finally
            {
                if (isNeedToRestoreConverter)
                    jsonSerializer.Converters.Add(source);
                if (newContractResolver != null)
                    jsonSerializer.ContractResolver = oldContractResolver;
            }
        }
    }
}