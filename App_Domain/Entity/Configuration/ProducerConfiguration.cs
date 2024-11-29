using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Xenia.IaA.AppDomain.Entity.Model;

namespace Xenia.IaA.AppDomain.Entity.Configuration;
internal class ProducerConfiguration : IEntityTypeConfiguration<Producer>
{
    public void Configure(EntityTypeBuilder<Producer> builder)
    {
        builder.HasKey(producer => producer.ID);
        builder.Property(producer => producer.ID).ValueGeneratedOnAdd();
        builder.HasIndex(producer => producer.Name).IsUnique();
        builder.Property(producer => producer.Name).IsUnicode().HasMaxLength(60).IsRequired();
    }
}