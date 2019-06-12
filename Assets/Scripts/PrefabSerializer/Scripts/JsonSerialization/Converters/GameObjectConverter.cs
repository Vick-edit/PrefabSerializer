using System;
using Newtonsoft.Json;
using PrefabSerializer.Scripts.Extensions;
using UnityEngine;

namespace PrefabSerializer.Scripts.JsonSerialization.Converters
{
    internal class GameObjectConverter : JsonConverter<GameObject>
    {
        public override void WriteJson(JsonWriter writer, GameObject value, JsonSerializer serializer)
        {
            var gameObjectJson = serializer.GetJsonString(value, this, new GameObjectContractResolver());
            var gameObjectWrapper = new GameObjectWrapper(gameObjectJson, value.transform);
            serializer.Serialize(writer, gameObjectWrapper, typeof(GameObjectWrapper));
        }

        public override GameObject ReadJson(JsonReader reader, Type objectType, GameObject existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var gameObjectWrapper = serializer.Deserialize<GameObjectWrapper>(reader);
            var gameObject = serializer.GetFromJson<GameObject>(gameObjectWrapper.OriginalEntry, this, new GameObjectContractResolver());

            gameObject.transform.position = gameObjectWrapper.OriginalTransform.position;
            gameObject.transform.rotation = gameObjectWrapper.OriginalTransform.rotation;
            gameObject.transform.localScale = gameObjectWrapper.OriginalTransform.localScale;

            return gameObject;
        }


        private class GameObjectWrapper
        {
            public string OriginalEntry;

            public Transform OriginalTransform;


            public GameObjectWrapper(string originalEntry, Transform originalTransform)
            {
                OriginalEntry = originalEntry;
                OriginalTransform = originalTransform;
            }
        }
    }
}