using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Xenia.IaA.AppDomain.Entity.Model;

namespace Xenia.IaA.AppDomain.Entity.Configuration;
internal class SaleConfiguration : IEntityTypeConfiguration<Sale>
{
    public void Configure(EntityTypeBuilder<Sale> builder)
    {
        builder.HasKey(sale => sale.ID);
        builder.Property(sale => sale.ID).ValueGeneratedOnAdd();
        builder.Property(sale => sale.TotalPriceInTRY).HasPrecision(10, 2).IsRequired();
        builder.Property(sale => sale.SaleConcluded).IsRequired();
        builder.Property(sale => sale.SaleStartDate).IsRequired();
        builder.Property(sale => sale.SaleConcludedDate).IsRequired(false);

        builder.HasOne(sale => sale.Customer)
            .WithMany(customer => customer.Sales)
            .HasForeignKey("CustomerID")
            .IsRequired();

        builder.HasMany(sale => sale.SaleItems)
            .WithOne(saleItem => saleItem.Sale)
            .HasForeignKey("SaleID")
            .IsRequired();

        builder.HasMany(sale => sale.Adjustments)
            .WithOne(adjustment => adjustment.Sale)
            .HasForeignKey("SaleID")
            .IsRequired();
    }
}