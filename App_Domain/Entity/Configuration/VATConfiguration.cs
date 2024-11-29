using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Xenia.IaA.AppDomain.Entity.Model;

namespace Xenia.IaA.AppDomain.Entity.Configuration;
internal class VATConfiguration : IEntityTypeConfiguration<VAT>
{
    public void Configure(EntityTypeBuilder<VAT> builder)
    {
        builder.HasKey(kdv => kdv.ID);
        builder.Property(kdv => kdv.ID).ValueGeneratedOnAdd();
        builder.Property(kdv => kdv.DefaultVAT).HasPrecision(6, 4).IsRequired();
    }
}