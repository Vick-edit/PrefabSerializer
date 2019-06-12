using System;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace PrefabSerializer.Scripts.Extensions
{
    public static class JsonSerializerExtensions
    {
        public static void SerializeOnOtherConverter(this JsonSerializer source, JsonWriter writer, object value, 
            JsonConverter converterToSkip, IContractResolver newContractResolver = null)
        {
            var isNeedToRestoreConverter = source.Converters.Any(c => c == converterToSkip);
            if (isNeedToRestoreConverter)
                source.Converters.Remove(converterToSkip);

            var oldContractResolver = source.ContractResolver;
            if (newContractResolver != null)
                source.ContractResolver = newContractResolver;

            try
            {
                source.Serialize(writer, value, value.GetType());
            }
            finally
            {
                if (isNeedToRestoreConverter)
                    source.Converters.Add(converterToSkip);
                if (newContractResolver != null)
                    source.ContractResolver = oldContractResolver;
            }
        }

        public static TEntity DeserializeOnOtherConverter<TEntity>(this JsonSerializer source, JsonReader reader,
            JsonConverter converterToSkip, IContractResolver newContractResolver = null)
        {
            var isNeedToRestoreConverter = source.Converters.Any(c => c == converterToSkip);
            if (isNeedToRestoreConverter)
                source.Converters.Remove(converterToSkip);

            var oldContractResolver = source.ContractResolver;
            if (newContractResolver != null)
                source.ContractResolver = newContractResolver;

            try
            {
                var entity = source.Deserialize<TEntity>(reader);
                return entity;
            }
            finally
            {
                if (isNeedToRestoreConverter)
                    source.Converters.Add(converterToSkip);
                if (newContractResolver != null)
                    source.ContractResolver = oldContractResolver;
            }
        }

        public static string GetJsonString(this JsonSerializer source, object value,
            JsonConverter converterToSkip, IContractResolver newContractResolver = null)
        {
            var isNeedToRestoreConverter = source.Converters.Any(c => c == converterToSkip);
            if (isNeedToRestoreConverter)
                source.Converters.Remove(converterToSkip);

            var oldContractResolver = source.ContractResolver;
            if (newContractResolver != null)
                source.ContractResolver = newContractResolver;

            try
            {
                return JsonConvert.SerializeObject(value, source.GetSettings());
            }
            finally
            {
                if (isNeedToRestoreConverter)
                    source.Converters.Add(converterToSkip);
                if (newContractResolver != null)
                    source.ContractResolver = oldContractResolver;
            }
        }


        public static TEntity GetFromJson<TEntity>(this JsonSerializer source, string json,
            JsonConverter converterToSkip, IContractResolver newContractResolver = null)
        {
            var isNeedToRestoreConverter = source.Converters.Any(c => c == converterToSkip);
            if (isNeedToRestoreConverter)
                source.Converters.Remove(converterToSkip);

            var oldContractResolver = source.ContractResolver;
            if (newContractResolver != null)
                source.ContractResolver = newContractResolver;

            try
            {
                return JsonConvert.DeserializeObject<TEntity>(json, source.GetSettings());
            }
            finally
            {
                if (isNeedToRestoreConverter)
                    source.Converters.Add(converterToSkip);
                if (newContractResolver != null)
                    source.ContractResolver = oldContractResolver;
            }
        }


        public static JsonSerializerSettings GetSettings(this JsonSerializer source)
        {
            var settings = new JsonSerializerSettings()
            {
                ContractResolver = source.ContractResolver,
                Formatting = source.Formatting,
                Converters = source.Converters,
                ReferenceLoopHandling = source.ReferenceLoopHandling,
                CheckAdditionalContent = source.CheckAdditionalContent,

                ConstructorHandling = source.ConstructorHandling,
                Context = source.Context,
                Culture = source.Culture,
                DateFormatHandling = source.DateFormatHandling,
                DateFormatString = source.DateFormatString,

                DateParseHandling = source.DateParseHandling,
                DateTimeZoneHandling = source.DateTimeZoneHandling,
                DefaultValueHandling = source.DefaultValueHandling,
                EqualityComparer = source.EqualityComparer,
                //Error = source.Error,

                FloatFormatHandling = source.FloatFormatHandling,
                FloatParseHandling = source.FloatParseHandling,
                MaxDepth = source.MaxDepth,
                MetadataPropertyHandling = source.MetadataPropertyHandling,
                MissingMemberHandling = source.MissingMemberHandling,

                NullValueHandling = source.NullValueHandling,
                ObjectCreationHandling = source.ObjectCreationHandling,
                PreserveReferencesHandling = source.PreserveReferencesHandling,
                //ReferenceResolverProvider = source.ReferenceResolverProvider,
                SerializationBinder = source.SerializationBinder,

                StringEscapeHandling = source.StringEscapeHandling,
                TraceWriter = source.TraceWriter,
                TypeNameAssemblyFormatHandling = source.TypeNameAssemblyFormatHandling,
                TypeNameHandling = source.TypeNameHandling,
            };
            return settings;
        }
    }
}