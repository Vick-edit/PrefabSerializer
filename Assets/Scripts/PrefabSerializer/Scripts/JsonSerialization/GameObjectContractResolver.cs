using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using UnityEngine;

namespace PrefabSerializer.Scripts.JsonSerialization
{
    internal class GameObjectContractResolver : DefaultContractResolver
    {
        private const string PROPERTY_TO_EXCLUDE = "scene";

        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            var baseProperties = base.CreateProperties(type, memberSerialization).ToList();

            var obsoleteProperties = type
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(property => property.IsDefined(typeof(ObsoleteAttribute), true))
                .Select(property => property.Name);
            baseProperties.RemoveAll(x => obsoleteProperties.Contains(x.PropertyName));

            baseProperties.RemoveAll(x => x.PropertyName == PROPERTY_TO_EXCLUDE);

            return baseProperties;
        }
    }
}