using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Xenia.IaA.AppDomain.Entity.Model;

namespace Xenia.IaA.AppDomain.Entity.Configuration;
internal class AdjustmentConfiguration : IEntityTypeConfiguration<Adjustment>
{
    public void Configure(EntityTypeBuilder<Adjustment> builder)
    {
        builder.HasKey(adjustment => adjustment.ID);
        builder.Property(adjustment => adjustment.ID).ValueGeneratedOnAdd();
        builder.Property(adjustment => adjustment.Name).IsUnicode().HasMaxLength(60).IsRequired();
        builder.Property(adjustment => adjustment.Description).IsUnicode().HasMaxLength(255).IsRequired(false);
        builder.Property(adjustment => adjustment.AdjustmentPriceInTRY).HasPrecision(10, 2).IsRequired();
        builder.Property(adjustment => adjustment.AdjustmentDate).IsRequired();
    }
}