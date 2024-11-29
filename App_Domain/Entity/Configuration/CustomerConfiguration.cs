using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Xenia.IaA.AppDomain.Entity.Model;

namespace Xenia.IaA.AppDomain.Entity.Configuration;
internal class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(customer => customer.ID);
        builder.Property(customer => customer.ID).ValueGeneratedOnAdd();
        builder.Property(customer => customer.Name).IsUnicode().HasMaxLength(60).IsRequired();
        builder.Property(customer => customer.BusinessName).IsUnicode().HasMaxLength(60).IsRequired(false);
        builder.Property(customer => customer.EmailAddress).IsUnicode(false).HasMaxLength(60).IsRequired(false);
        builder.Property(customer => customer.PhoneNumber).IsUnicode(false).HasMaxLength(19).IsRequired(false);
        builder.Property(customer => customer.Address).IsUnicode().HasMaxLength(255).IsRequired(false);
    }
}