using System;
using Newtonsoft.Json;
using PrefabSerializer.Scripts.Extensions;
using UnityEngine;

namespace PrefabSerializer.Scripts.JsonSerialization.Converters
{
    internal class DefaultComponentConverter : JsonConverter<Component>
    {
        public override void WriteJson(JsonWriter writer, Component value, JsonSerializer serializer)
        {
            try
            {
                var jsonString = JsonUtility.ToJson(value);
                writer.WriteValue(jsonString);
            }
            catch (Exception innerException)
            {
                Debug.LogWarning($"Возникла ошибка при сериализации объекта типа {value.GetType()} силами {nameof(JsonUtility)}.\r\n" +
                                 $"Текст оригинальней ошибки: {innerException.Message}.\r\n" +
                                 $"Пробую использовать {nameof(JsonUtility)}");

                serializer.SerializeOnOtherConverter(writer, value, this, new ComponentContractResolver());
            }
        }

        public override Component ReadJson(JsonReader reader, Type objectType, Component existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            try
            {
                var jsonString = (string)reader.Value;
                var deserializedValue = (Component)JsonUtility.FromJson(jsonString, objectType);
                return deserializedValue;
            }
            catch (Exception innerException)
            {
                Debug.LogWarning($"Возникла ошибка при десериализации объекта типа {objectType} силами {nameof(JsonUtility)}.\r\n" +
                                 $"Текст оригинальней ошибки: {innerException.Message}.\r\n" +
                                 $"Пробую использовать {nameof(JsonUtility)}");

                var deserializedValue = serializer.DeserializeOnOtherConverter<Component>(reader, this, new ComponentContractResolver());
                return deserializedValue;
            }
        }
    }
}