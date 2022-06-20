using MealPlanner.Core;
using Microsoft.EntityFrameworkCore;

namespace MealPlanner.Data
{
    public class MealPlannerDbContext : DbContext
    {
        public MealPlannerDbContext(DbContextOptions<MealPlannerDbContext> options)
            : base(options)
        {
            Database.Migrate();
        }

        public DbSet<Recipe> Recipes { get; set; }
    }
}
