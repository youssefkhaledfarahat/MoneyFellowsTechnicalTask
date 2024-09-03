using Microsoft.EntityFrameworkCore;
using MoneyFellows.Products.Core.Entities;
using MoneyFellows.Products.Infrastructure.Configurations;

namespace MoneyFellows.Products.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateAuditFields();
            return base.SaveChangesAsync();
        }

        private void UpdateAuditFields()
        {
            var entries = ChangeTracker.Entries()
                .Where(e => e.Entity is BaseEntity &&
                            (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entry in entries)
            {
                var entity = (BaseEntity)entry.Entity;

                if (entry.State == EntityState.Added)
                {
                    entity.CreatedAt = DateTime.UtcNow;
                    entity.CreatedBy = "JWT Token UserName";
                }
                else if (entry.State == EntityState.Modified)
                {
                    entity.UpdatedAt = DateTime.UtcNow;
                    entity.UpdatedBy = "JWT Token UserName (Update)";
                }
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ProductConfiguration());

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}
