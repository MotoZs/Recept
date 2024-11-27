namespace RecipeApp.Entities
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }
        public List<Ingredient> Ingredients { get; set; } = new();
        public List<Rating> Ratings { get; set; } = new();
    }
}
