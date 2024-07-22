using Microsoft.EntityFrameworkCore;
using TestingAkbas.Models;

namespace TestingAkbas.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Fabric> Fabrics { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Fabric>(entity =>
            {
                entity.Property(e => e.RawFabricPrice).HasColumnType("decimal(18,2)");
                entity.Property(e => e.DomesticPrice).HasColumnType("decimal(18,2)");
                entity.Property(e => e.ExportPrice).HasColumnType("decimal(18,2)");
            });
        }
    }
}
