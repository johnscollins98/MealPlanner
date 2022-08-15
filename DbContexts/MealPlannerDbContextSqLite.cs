using Microsoft.EntityFrameworkCore;

namespace MealPlanner;
public class MealPlannerDbContextSqLite : DbContext
{
  public MealPlannerDbContextSqLite(DbContextOptions<MealPlannerDbContextSqLite> options)
      : base(options)
  {
    Database.Migrate();
  }

  public DbSet<RecipeEntity> Recipes => Set<RecipeEntity>();
  public DbSet<MealPlanEntity> MealPlans => Set<MealPlanEntity>();
}
