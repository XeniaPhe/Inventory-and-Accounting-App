using System.Reflection;
using System.Text.Json;

namespace Xenia.IaA.ResourceManagement;
public class EmbeddedJsonReader : EmbeddedResourceReader
{
    public EmbeddedJsonReader(Assembly assembly, string resourcePath) : base(assembly, resourcePath) { }

    public string this[string key]
    {
        get
        {
            if (key is null or "")
                throw new ArgumentException("Key cannot be null or empty!");

            string[] keyHierarchy = key.Split(':');

            using JsonDocument doc = JsonDocument.Parse(content);
            JsonElement iterator = doc.RootElement;

            foreach (string item in keyHierarchy)
            {
                if (!iterator.TryGetProperty(item, out iterator))
                {
                    throw new ArgumentException($"Key {key} not found in the JSON data!");
                }
            }

            return iterator.ToString();
        }
    }
}
