namespace PrefabSerializer.Scripts.JsonSerialization
{
    public interface IJsonSerializer
    {
        string Serialize(object value, bool compressJson = true);

        TValue Deserialize<TValue>(string jsonValue);
    }
}