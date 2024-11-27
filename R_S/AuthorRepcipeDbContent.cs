using Microsoft.EntityFrameworkCore;

namespace Recept
{
    public class AuthorRepcipeDbContent : DbContent
    {
        public DbSet<Author> Authors {  get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Rating> Ratings { get; set; }
    }
}
