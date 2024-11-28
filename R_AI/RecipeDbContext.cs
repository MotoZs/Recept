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
                new Author { Id = 2, Name = "Csíkos Marcell" },
                new Author { Id = 3, Name = "Czesznak Attila" },
                new Author { Id = 4, Name = "Vida Tamás" },
                new Author { Id = 5, Name = "Papdi Zsombor" }
            );

            modelBuilder.Entity<Recipe>().HasData(
                new Recipe { Id = 1, Title = "Marhapörkölt", Description = "A jó húsoknak ;)", AuthorId = 1 },
                new Recipe { Id = 2, Title = "Túró rudi", Description = "Full eredeti", AuthorId = 2 },
                new Recipe { Id = 3, Title = "Tészta leves", Description = "Sok tésztával", AuthorId = 3 },
                new Recipe { Id = 4, Title = "Aranygaluska", Description = "Mindenki kecvence", AuthorId = 4 },
                new Recipe { Id = 5, Title = "Palacsinta", Description = "Nagyi féle", AuthorId = 5 },
                new Recipe { Id = 6, Title = "Rántott hús", Description = "Csakis csirkéből", AuthorId = 5 }
            );

            modelBuilder.Entity<Ingredient>().HasData(
                new Ingredient { Id = 1, Name = "Túró", Quantity = "200g", RecipeId = 1 },
                new Ingredient { Id = 2, Name = "Rudi", Quantity = "5 db", RecipeId = 1 },
                new Ingredient { Id = 3, Name = "Tészta", Quantity = "500g", RecipeId = 2 },
                new Ingredient { Id = 4, Name = "Marha hús", Quantity = "2kg", RecipeId = 2 },
                new Ingredient { Id = 5, Name = "Galuska", Quantity = "6db", RecipeId = 4 },
                new Ingredient { Id = 6, Name = "Tojás", Quantity = "8 db", RecipeId = 5 },
                new Ingredient { Id = 7, Name = "Csirke hús", Quantity = "400g", RecipeId = 6 },
                new Ingredient { Id = 8, Name = "Víz", Quantity = "1.5l", RecipeId = 3 }
            );
        }
    }
}
