using Xenia.IaA.AppDomain.Entity.Model;
using Microsoft.EntityFrameworkCore;
using Xenia.IaA.AppDomain.Entity.Configuration;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Xenia.IaA.AppDomain.Persistence.Context;
public class ApplicationDbContext : DbContext
{
    internal virtual DbSet<Product> Products { get; set; }
    internal virtual DbSet<Producer> Producers { get; set; }
    internal virtual DbSet<Currency> Currencies { get; set; }
    internal virtual DbSet<Sale> Sales { get; set; }
    internal virtual DbSet<SaleItem> SaleItems { get; set; }
    internal virtual DbSet<Adjustment> Adjustments { get; set; }
    internal virtual DbSet<Customer> Customers { get; set; }
    internal virtual DbSet<VAT> KDV { get; set; }

    internal ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ProductConfiguration());
        modelBuilder.ApplyConfiguration(new CategoryConfiguration());
        modelBuilder.ApplyConfiguration(new ProducerConfiguration());
        modelBuilder.ApplyConfiguration(new CurrencyConfiguration());
        modelBuilder.ApplyConfiguration(new SaleConfiguration());
        modelBuilder.ApplyConfiguration(new SaleItemConfiguration());
        modelBuilder.ApplyConfiguration(new AdjustmentConfiguration());
        modelBuilder.ApplyConfiguration(new CustomerConfiguration());
        modelBuilder.ApplyConfiguration(new VATConfiguration());
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<DateTime>().HaveConversion<DateTimeToLongValueConverter>();
        configurationBuilder.Properties<bool>().HaveConversion<BoolToZeroOneConverter<int>>();
    }
}