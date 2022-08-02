using MealPlanner.Core;
using Microsoft.EntityFrameworkCore;

namespace MealPlanner.Data
{
    public class MealPlannerDbContextSqLite : DbContext
    {
        public MealPlannerDbContextSqLite(DbContextOptions<MealPlannerDbContextSqLite> options)
            : base(options)
        {
            Database.Migrate();
        }

        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<MealPlan> MealPlans { get; set; }
    }
}
