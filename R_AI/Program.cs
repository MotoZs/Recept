using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using RecipeApp.DbContext;
using RecipeApp.Entities;
using RecipeApp.Enums;

namespace RecipeApp
{
    class Program
    {
        static void Main()
        {
            using var context = new RecipeDbContext();
            context.Database.EnsureCreated();

            while (true)
            {

                Console.WriteLine("\nVálassz:");
                Console.WriteLine($"{(int)Menu.ListRecipes}. Receptek");
                Console.WriteLine($"{(int)Menu.AddRecipe}. Új recept hozzáadása");
                Console.WriteLine($"{(int)Menu.AddRating}. Értékelés");
                Console.WriteLine($"{(int)Menu.Exit}. Kilépés");


                Console.Write("Válasz: ");
                var input = Console.ReadLine();
                var menu = Enum.Parse(typeof(Menu), input);
                switch (menu)
                {
                        case Menu.ListRecipes:
                            ListRecipes(context);
                            break;
                        case Menu.AddRecipe:
                            AddRecipe(context);
                            break;
                        case Menu.AddRating:
                            AddRating(context);
                            break;
                        case Menu.Exit:
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("HIBA!");
                            break;
                }
            }
        }
        

        static void ListRecipes(RecipeDbContext context)
        {
            var recipes = context.Recipes
                .Include(r => r.Author)
                .Include(r => r.Ingredients)
                .Include(r => r.Ratings)
                .ToList();

            foreach (var recipe in recipes)
            {
                Console.WriteLine($"\n{recipe.Title} | szerző: {recipe.Author.Name}");
                Console.WriteLine($"Leírás: {recipe.Description}");
                Console.WriteLine("Hozzávalók:");
                foreach (var ingredient in recipe.Ingredients)
                    Console.WriteLine($" - {ingredient.Name}: {ingredient.Quantity}");
                Console.WriteLine("Értékelés:");
                foreach (var rating in recipe.Ratings)
                    Console.WriteLine($" - {rating.Score}/5: {rating.Comment}");
            }
        }

        static void AddRecipe(RecipeDbContext context)
        {
            Console.Write("Add meg a nevét: ");
            var title = Console.ReadLine();
            Console.Write("Add meg a leírását: ");
            var description = Console.ReadLine();

            Console.Write("Szerző ID-ja: ");
            if (!int.TryParse(Console.ReadLine(), out var authorId))
            {
                Console.WriteLine("Nincs ilyen szerző ID!");
                return;
            }

            var recipe = new Recipe
            {
                Title = title,
                Description = description,
                AuthorId = authorId
            };

            Console.Write("Hány féle hozzávaló kell hozzá: ");
            if (int.TryParse(Console.ReadLine(), out var ingredientCount))
            {
                for (int i = 0; i < ingredientCount; i++)
                {
                    Console.Write("Miből: ");
                    var name = Console.ReadLine();
                    Console.Write("Mennyit: ");
                    var quantity = Console.ReadLine();

                    recipe.Ingredients.Add(new Ingredient { Name = name, Quantity = quantity });
                }
            }

            context.Recipes.Add(recipe);
            context.SaveChanges();
            Console.WriteLine("Recept sikeresen hozzáadva!❤");
        }

        static void AddRating(RecipeDbContext context)
        {
            Console.Write("Add meg a recept ID-ját: ");
            if (!int.TryParse(Console.ReadLine(), out var recipeId))
            {
                Console.WriteLine("Nincs ilyen recept ID!");
                return;
            }

            Console.Write("Értékeld (1-5): ");
            if (!int.TryParse(Console.ReadLine(), out var score) || score < 1 || score > 5)
            {
                Console.WriteLine("HIBÁS érték!");
                return;
            }

            Console.Write("Komment: ");
            var comment = Console.ReadLine();

            var rating = new Rating
            {
                RecipeId = recipeId,
                Score = score,
                Comment = comment
            };

            context.Ratings.Add(rating);
            context.SaveChanges();
            Console.WriteLine("Értékelés sikeresen hozzáadva!😎");
        }
    }
}
