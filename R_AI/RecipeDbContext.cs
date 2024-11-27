using Microsoft.EntityFrameworkCore;
using RecipeApp.Entities;

namespace RecipeApp.DbContext
{
    public class RecipeDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Rating> Ratings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
           => options.UseSqlServer($"Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=Recept;Integrated Security=true");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>().HasData(
                new Author { Id = 1, Name = "John Doe" },
                new Author { Id = 2, Name = "Jane Smith" }
            );

            modelBuilder.Entity<Recipe>().HasData(
                new Recipe { Id = 1, Title = "Pancakes", Description = "Fluffy pancakes", AuthorId = 1 },
                new Recipe { Id = 2, Title = "Spaghetti Bolognese", Description = "Classic Italian dish", AuthorId = 2 }
            );

            modelBuilder.Entity<Ingredient>().HasData(
                new Ingredient { Id = 1, Name = "Flour", Quantity = "200g", RecipeId = 1 },
                new Ingredient { Id = 2, Name = "Eggs", Quantity = "2 pcs", RecipeId = 1 },
                new Ingredient { Id = 3, Name = "Spaghetti", Quantity = "300g", RecipeId = 2 },
                new Ingredient { Id = 4, Name = "Ground beef", Quantity = "400g", RecipeId = 2 }
            );
        }
    }
}
