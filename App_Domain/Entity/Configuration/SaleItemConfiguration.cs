using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Xenia.IaA.AppDomain.Entity.Model;

namespace Xenia.IaA.AppDomain.Entity.Configuration;
internal class SaleItemConfiguration : IEntityTypeConfiguration<SaleItem>
{
    public void Configure(EntityTypeBuilder<SaleItem> builder)
    {
        builder.HasKey(saleItem => saleItem.ID);
        builder.Property(saleItem => saleItem.ID).ValueGeneratedOnAdd();
        builder.Property(saleItem => saleItem.ProductBuyingPrice).HasPrecision(10, 2).IsRequired();
        builder.Property(saleItem => saleItem.ProductSellingPrice).HasPrecision(10, 2).IsRequired();
        builder.Property(saleItem => saleItem.CurrencyRateToTRY).HasPrecision(10, 4).IsRequired();
        builder.Property(saleItem => saleItem.Amount).IsRequired();
        builder.Property(saleItem => saleItem.DiscountPercentage).HasPrecision(4, 2).IsRequired();
        builder.Property(saleItem => saleItem.VAT).HasPrecision(6, 4).IsRequired();
        builder.Property(saleItem => saleItem.TotalPriceInTRY).HasPrecision(10, 2).IsRequired();
        builder.Property(saleItem => saleItem.SaleDate).IsRequired();

        builder.HasOne(saleItem => saleItem.Currency)
            .WithMany()
            .HasForeignKey("CurrencyISOCode")
            .IsRequired();
    }
}