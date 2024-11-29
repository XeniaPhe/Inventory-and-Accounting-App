using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Xenia.IaA.AppDomain.Entity.Model;

namespace Xenia.IaA.AppDomain.Entity.Configuration;
internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(category => category.ID);
        builder.Property(category => category.ID).ValueGeneratedOnAdd();
        builder.HasIndex(category => category.Name).IsUnique();
        builder.Property(category => category.Name).IsUnicode().HasMaxLength(60).IsRequired();
        builder.Property(category => category.Description).IsUnicode().HasMaxLength(255).IsRequired(false);
    }
}