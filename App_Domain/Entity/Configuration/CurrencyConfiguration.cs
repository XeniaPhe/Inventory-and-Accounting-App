using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Xenia.IaA.AppDomain.Entity.Model;

namespace Xenia.IaA.AppDomain.Entity.Configuration;
internal class CurrencyConfiguration : IEntityTypeConfiguration<Currency>
{
    public void Configure(EntityTypeBuilder<Currency> builder)
    {
        builder.HasKey(currency => currency.ISOCode);
        builder.Property(currency => currency.ISOCode).IsUnicode(false).IsFixedLength().HasMaxLength(3);
        builder.HasIndex(currency => currency.Name).IsUnique();
        builder.Property(currency => currency.Name).IsUnicode(false).HasMaxLength(60).IsRequired();
        builder.Property(currency => currency.ExchangeRateToTRY).HasPrecision(10, 4).IsRequired();
        builder.Property(currency => currency.RateTimestamp).IsRequired();
    }
}