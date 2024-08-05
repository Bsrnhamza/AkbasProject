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

        public DbSet<Fabric> Fabrics { get; set; } // boyahana genel fiyat listesi
        public DbSet<User> Users { get; set; } // Kullanıcı tablosu ile bağlantı
    }
}
