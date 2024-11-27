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
                new Author { Id = 1, Name = "Bódis Bálint" },
                new Author { Id = 2, Name = "Csíkos Marcell" }
            );

            modelBuilder.Entity<Recipe>().HasData(
                new Recipe { Id = 1, Title = "Marhapörkölt", Description = "A jó húsoknak ;)", AuthorId = 1 },
                new Recipe { Id = 2, Title = "Túró rudi", Description = "Full eredeti", AuthorId = 2 }
            );

            modelBuilder.Entity<Ingredient>().HasData(
                new Ingredient { Id = 1, Name = "Túró", Quantity = "200g", RecipeId = 1 },
                new Ingredient { Id = 2, Name = "Rudi", Quantity = "5 db", RecipeId = 1 },
                new Ingredient { Id = 3, Name = "Tészta", Quantity = "300g", RecipeId = 2 },
                new Ingredient { Id = 4, Name = "Marha hús", Quantity = "400g", RecipeId = 2 }
            );
        }
    }
}
