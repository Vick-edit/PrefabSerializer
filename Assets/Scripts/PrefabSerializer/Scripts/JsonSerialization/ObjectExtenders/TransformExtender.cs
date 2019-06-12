using UnityEngine;
using UnityEngine.UIElements;

namespace PrefabSerializer.Scripts.JsonSerialization.ObjectExtenders
{
    public class TransformExtender : Transform
    {
        public TransformExtender(Vector3 position, Quaternion rotation, Vector3 scale)
        {
            this.position = position;
            this.rotation = rotation;
            this.localScale = scale;
        }
    }
}