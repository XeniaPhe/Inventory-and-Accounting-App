using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Xenia.IaA.AppDomain.Persistence.Context;
internal class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder.UseSqlite(DatabaseManager.GetConnectionString());
        return new ApplicationDbContext(optionsBuilder.Options);
    }
}
