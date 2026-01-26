using Microsoft.EntityFrameworkCore;
using Proyecto_Final_ProgramacionWEB.Entities;

namespace Proyecto_Final_ProgramacionWEB.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options): base(options) 
        { 
        }
        //public DbSet<Gastronomy> Gastronomies { get; set; } = null!;
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<Gastronomy>()
            //  .HasMany(g => g.Restaurants)
            //  .WithOne(r => r.Gastronomy)
            //  .HasForeignKey(r => r.Id_Gastronomy)
            //  .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Restaurant>()
                .HasMany(r => r.Categories)
                .WithOne(c => c.Restaurant)
                .HasForeignKey(c => c.Id_Restaurant)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Category>()
                .HasMany(c => c.Products)
                .WithOne(p => p.Category)
                .HasForeignKey(p => p.Id_Category)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
