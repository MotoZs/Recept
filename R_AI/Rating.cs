namespace RecipeApp.Entities
{
    public class Rating
    {
        public int Id { get; set; }
        public int Score { get; set; }
        public string Comment { get; set; }
        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }
    }
}
