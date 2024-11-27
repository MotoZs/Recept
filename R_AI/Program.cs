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

                Console.WriteLine("\nMenu:");
                Console.WriteLine($"{(int)Menu.ListRecipes}. List Recipes");
                Console.WriteLine($"{(int)Menu.AddRecipe}. Add New Recipe");
                Console.WriteLine($"{(int)Menu.AddRating}. Add Rating to a Recipe");
                Console.WriteLine($"{(int)Menu.Exit}. Exit");


                Console.Write("Choose an option: ");
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
                            Console.WriteLine("Invalid option. Try again.");
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
                Console.WriteLine($"\n{recipe.Title} by {recipe.Author.Name}");
                Console.WriteLine($"Description: {recipe.Description}");
                Console.WriteLine("Ingredients:");
                foreach (var ingredient in recipe.Ingredients)
                    Console.WriteLine($" - {ingredient.Name}: {ingredient.Quantity}");
                Console.WriteLine("Ratings:");
                foreach (var rating in recipe.Ratings)
                    Console.WriteLine($" - {rating.Score}/5: {rating.Comment}");
            }
        }

        static void AddRecipe(RecipeDbContext context)
        {
            Console.Write("Enter recipe title: ");
            var title = Console.ReadLine();
            Console.Write("Enter recipe description: ");
            var description = Console.ReadLine();

            Console.Write("Enter author ID: ");
            if (!int.TryParse(Console.ReadLine(), out var authorId))
            {
                Console.WriteLine("Invalid author ID.");
                return;
            }

            var recipe = new Recipe
            {
                Title = title,
                Description = description,
                AuthorId = authorId
            };

            Console.Write("Enter number of ingredients: ");
            if (int.TryParse(Console.ReadLine(), out var ingredientCount))
            {
                for (int i = 0; i < ingredientCount; i++)
                {
                    Console.Write("Ingredient name: ");
                    var name = Console.ReadLine();
                    Console.Write("Ingredient quantity: ");
                    var quantity = Console.ReadLine();

                    recipe.Ingredients.Add(new Ingredient { Name = name, Quantity = quantity });
                }
            }

            context.Recipes.Add(recipe);
            context.SaveChanges();
            Console.WriteLine("Recipe added successfully!");
        }

        static void AddRating(RecipeDbContext context)
        {
            Console.Write("Enter recipe ID: ");
            if (!int.TryParse(Console.ReadLine(), out var recipeId))
            {
                Console.WriteLine("Invalid recipe ID.");
                return;
            }

            Console.Write("Enter rating (1-5): ");
            if (!int.TryParse(Console.ReadLine(), out var score) || score < 1 || score > 5)
            {
                Console.WriteLine("Invalid rating.");
                return;
            }

            Console.Write("Enter comment: ");
            var comment = Console.ReadLine();

            var rating = new Rating
            {
                RecipeId = recipeId,
                Score = score,
                Comment = comment
            };

            context.Ratings.Add(rating);
            context.SaveChanges();
            Console.WriteLine("Rating added successfully!");
        }
    }
}
