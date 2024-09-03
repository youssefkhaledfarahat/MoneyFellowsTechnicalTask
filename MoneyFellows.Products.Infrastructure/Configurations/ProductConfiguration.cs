using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoneyFellows.Products.Core.Entities;

namespace MoneyFellows.Products.Infrastructure.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.Description)
                .HasMaxLength(500);

            builder.Property(p => p.Price)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.HasIndex(p => p.Name).IsUnique();

            // Base entity configurations
            builder.Property(p => p.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

            builder.Property(p => p.CreatedBy)
                .HasMaxLength(100);

            builder.Property(p => p.UpdatedAt)
                .IsRequired(false);

            builder.Property(p => p.UpdatedBy)
                .HasMaxLength(100);
        }
    }
}
