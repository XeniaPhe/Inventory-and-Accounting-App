using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Xenia.IaA.AppDomain.Entity.Model;

namespace Xenia.IaA.AppDomain.Entity.Configuration;
internal class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(product => product.ProductCode);
        builder.HasIndex(product => product.Name).IsUnique();
        builder.Property(product => product.Name).IsUnicode().HasMaxLength(60).IsRequired();
        builder.Property(product => product.Description).IsUnicode().HasMaxLength(255).IsRequired(false);
        builder.Property(product => product.BuyingPrice).HasPrecision(10, 2).IsRequired();
        builder.Property(product => product.SellingPrice).HasPrecision(10, 2).IsRequired();
        builder.Property(product => product.Stock).IsRequired();
        builder.Property(product => product.LastPriceUpdateDate).IsRequired();

        builder.HasOne(product => product.Category)
            .WithMany(category => category.Products)
            .HasForeignKey("CategoryID")
            .IsRequired(false);

        builder.HasOne(product => product.Producer)
            .WithMany(producer => producer.Products)
            .HasForeignKey("ProducerID")
            .IsRequired(false);

        builder.HasOne(product => product.Currency)
            .WithMany()
            .HasForeignKey("CurrencyISOCode")
            .IsRequired();

        builder.HasMany(product => product.Sales)
            .WithOne(saleItem => saleItem.Product)
            .HasForeignKey("ProductCode")
            .IsRequired();
    }
}