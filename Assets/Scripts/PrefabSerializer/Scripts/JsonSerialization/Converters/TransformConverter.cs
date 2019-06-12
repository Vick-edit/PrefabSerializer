using System;
using Newtonsoft.Json;
using PrefabSerializer.Scripts.JsonSerialization.ObjectExtenders;
using UnityEngine;
using Object = UnityEngine.Object;

namespace PrefabSerializer.Scripts.JsonSerialization.Converters
{
    internal class TransformConverter : JsonConverter<Transform>
    {
        public override void WriteJson(JsonWriter writer, Transform value, JsonSerializer serializer)
        {
            var transformWrapper = new TransformWrapper(value);
            var wrapperJson = JsonUtility.ToJson(transformWrapper);
            writer.WriteValue(wrapperJson);
        }

        public override Transform ReadJson(JsonReader reader, Type objectType, Transform existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var wrapperJson = (string)reader.Value;
            var transformWrapper = JsonUtility.FromJson<TransformWrapper>(wrapperJson);
            var transform = transformWrapper.ExtractTransform();
            return transform;
        }


        private class TransformWrapper
        {
            public Vector3 Position;
            public Quaternion Rotation;
            public Vector3 Scale;

            public TransformWrapper(Transform sourceTransform)
            {
                Position = sourceTransform.position;
                Rotation = sourceTransform.rotation;
                Scale = sourceTransform.localScale;
            }

            public Transform ExtractTransform()
            {
                var transform = new TransformExtender(Position, Rotation, Scale);
                return transform;
            }
        }
    }
}