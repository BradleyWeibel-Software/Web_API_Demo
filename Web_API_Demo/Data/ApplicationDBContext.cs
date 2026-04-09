using Microsoft.EntityFrameworkCore;
using Web_API_Demo.Model;

namespace Web_API_Demo.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) { }
        public DbSet<Shirt> Shirts { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Data seeding
            modelBuilder.Entity<Shirt>().HasData(
                new Shirt() { Id = 1, Brand = "Nike", Colour = "Black", Size = 10, Sex = "Female" },
                new Shirt() { Id = 2, Brand = "Puma", Colour = "Red", Size = 12, Sex = "Male" },
                new Shirt() { Id = 3, Brand = "RHCP", Colour = "Grey", Size = 10, Sex = "Female" },
                new Shirt() { Id = 4, Brand = "TBK", Colour = "Pink", Size = 6, Sex = "Female" },
                new Shirt() { Id = 5, Brand = "TBS", Colour = "Black", Size = 8, Sex = "Male" },
                new Shirt() { Id = 6, Brand = "DC", Colour = "White", Size = 11, Sex = "Male" },
                new Shirt() { Id = 7, Brand = "Adidas", Colour = "Grey", Size = 10, Sex = "Female" },
                new Shirt() { Id = 8, Brand = "[no-name]", Colour = "White", Size = 12, Sex = "Male" },
                new Shirt() { Id = 9, Brand = "Nike", Colour = "Gold", Size = 10, Sex = "Female" },
                new Shirt() { Id = 10, Brand = "RHCP", Colour = "Red", Size = 10, Sex = "Female" }
            );
        }
    }
}
