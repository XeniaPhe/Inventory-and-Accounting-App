using System.Text;
using System.Text.RegularExpressions;

namespace Xenia.IaA.ResourceManagement;
public class ResourcePathBuilder
{
    private string rootNamespace;
    private string resourceName;
    private List<string> folderHierarchy;

    public ResourcePathBuilder()
    {
        this.rootNamespace = string.Empty;
        this.resourceName = string.Empty;
        this.folderHierarchy = new List<string>();
    }

    public ResourcePathBuilder RootNamespace(string rootNamespace)
    {
        if (!this.rootNamespace.Equals(string.Empty))
        {
            throw new InvalidOperationException("Root namespace is already set!");
        }

        this.rootNamespace = rootNamespace;
        return this;
    }

    public ResourcePathBuilder ResourceName(string resourceName)
    {
        if (!this.resourceName.Equals(string.Empty))
        {
            throw new InvalidOperationException("Resource name is already set!");
        }

        this.resourceName = resourceName;
        return this;
    }

    public ResourcePathBuilder AddFolderToHierarchy(string folderName)
    {
        folderHierarchy.Add(folderName);
        return this;
    }

    private string CleanseName(string name)
    {
        if (name is null or "")
        {
            throw new InvalidDataException("Names cannot be null or empty!");
        }

        if (char.IsNumber(name[0]))
        {
            name = $"_{name}";
        }

        return Regex.Replace(name, @"\s+", "_");
    }

    public string Build()
    {
        if (rootNamespace.Equals(string.Empty) || resourceName.Equals(string.Empty))
        {
            throw new InvalidDataException("Not all the required fields are set!");
        }

        StringBuilder sb = new StringBuilder(CleanseName(rootNamespace));

        foreach (var folder in folderHierarchy)
        {
            sb.Append($".{CleanseName(folder)}");
        }

        sb.Append($".{CleanseName(resourceName)}");

        return sb.ToString();
    }
}