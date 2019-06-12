using System.IO;
using System.Xml;
using PrefabSerializer.Scripts.JsonSerialization;
using UnityEditor;
using UnityEngine;

namespace PrefabSerializer.Scripts
{
    public static class Tests
    {
        private static string TEMP_FOLDER = "PrefabSerialization";

        [MenuItem("Assets/TestSerialization")]
        public static void TestSerialization()
        {
            var tempContainerPath = Application.temporaryCachePath;
            var tempFolder = Path.Combine(tempContainerPath, TEMP_FOLDER);

            var selectedGameObject = Selection.activeGameObject;
            var jsonSerializer = new UnityJsonSerializer();
            var jsonText = jsonSerializer.Serialize(selectedGameObject, false);
            Debug.LogWarning(jsonText);

            var deserialized = jsonSerializer.Deserialize<GameObject>(jsonText);
            jsonText = jsonSerializer.Serialize(deserialized, false);
            Debug.LogWarning(jsonText);
        }
    }
}

