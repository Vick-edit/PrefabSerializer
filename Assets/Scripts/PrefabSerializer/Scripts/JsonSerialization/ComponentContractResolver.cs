using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using UnityEngine;

namespace PrefabSerializer.Scripts.JsonSerialization
{
    internal class ComponentContractResolver : DefaultContractResolver
    {
        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            var baseProperties = base.CreateProperties(type, memberSerialization).ToList();
            var allBasePropertiesNames = baseProperties.Select(x => x.PropertyName).ToArray();

            var obsoleteProperties = type
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(property => property.IsDefined(typeof(ObsoleteAttribute), true))
                .Select(property => property.Name);
            baseProperties.RemoveAll(x => obsoleteProperties.Contains(x.PropertyName));

            var unitySerializableFields = type
                .GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(field => !field.IsDefined(typeof(ObsoleteAttribute), true))
                .Where(field => field.IsDefined(typeof(SerializeField), true))
                .Where(field => allBasePropertiesNames.All(x => x != field.Name))
                .Select(field => base.CreateProperty(field, memberSerialization));
            baseProperties.AddRange(unitySerializableFields);

            return baseProperties;
        }
    }
}