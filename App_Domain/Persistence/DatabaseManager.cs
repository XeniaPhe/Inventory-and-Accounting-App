using System.Reflection;
using Xenia.IaA.ResourceManagement;

namespace Xenia.IaA.AppDomain.Persistence;
internal static class DatabaseManager
{
    private static EmbeddedJsonReader config;

    static DatabaseManager()
    {
        string configPath = new ResourcePathBuilder()
            .RootNamespace("App_Domain")
            .AddFolderToHierarchy("Resources")
            .ResourceName("connectionstrings.json")
            .Build();

        Assembly assembly = Assembly.GetExecutingAssembly();
        config = new EmbeddedJsonReader(assembly, configPath);
    }

    internal static string GetConnectionString()
    {
        string currentDir = System.AppDomain.CurrentDomain.BaseDirectory;
        string dbName = config["ConnectionStrings:SQLiteConnection:DatabaseName"];
        string dbPath = Path.Combine(currentDir, dbName);
        string connectionString = config["ConnectionStrings:SQLiteConnection:ConnectionString"];
        return connectionString.Replace("{DatabasePath}", dbPath);
    }
}