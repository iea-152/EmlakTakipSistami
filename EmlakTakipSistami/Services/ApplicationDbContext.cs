using EmlakTakipSistami.Models;
using Microsoft.EntityFrameworkCore;

namespace EmlakTakipSistami.Services
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Daire> Daireler { get; set; }
        public DbSet<Users> Users { get; set; }

        public DbSet<Kiraci> Kiracilar { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Daire>()
                .Property(d => d.KiraUcreti)
                .HasColumnType("decimal(18,2)");

            

        }

    }
}
 