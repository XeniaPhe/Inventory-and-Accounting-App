using System.Reflection;

namespace Xenia.IaA.ResourceManagement;
public class EmbeddedResourceReader
{
    protected string content;

    public EmbeddedResourceReader(Assembly assembly, string resourcePath)
    {
        if (assembly is null)
            throw new ArgumentNullException("Assembly cannot be null!");

        if (resourcePath is null or "")
            throw new ArgumentException("Resource path cannot be null or empty!");

        using Stream? stream = assembly.GetManifestResourceStream(resourcePath);

        if (stream is null)
            throw new FileNotFoundException($"Resource {resourcePath} not found!");

        using StreamReader reader = new StreamReader(stream);
        this.content = reader.ReadToEnd();
    }

    public string Read() => content;
}