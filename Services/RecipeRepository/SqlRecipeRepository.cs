using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Security.Claims;

namespace MealPlanner;
public class SqlRecipeRepository : IRecipeRepository
{
  private readonly MealPlannerDbContextSqLite db;

  public SqlRecipeRepository(MealPlannerDbContextSqLite db)
  {
    this.db = db;
  }

  public RecipeEntity Add(RecipeEntity entity)
  {
    db.Add(entity);
    return entity;
  }

  public IEnumerable<RecipeEntity> All()
  {
    return db.Recipes.OrderBy(r => r.Time).ThenBy(r => r.Category).ThenBy(r => r.Name);
  }

  public int Commit()
  {
    return db.SaveChanges();
  }

  public RecipeEntity? Delete(int id)
  {
    var recipe = Get(id);
    if (recipe != null)
    {
      db.Recipes.Remove(recipe);
    }
    return recipe;
  }

  public IEnumerable<RecipeEntity> Find(Expression<Func<RecipeEntity, bool>> predicate)
  {
    var query = db.Recipes
        .Where(predicate)
        .OrderBy(r => r.Time)
        .ThenBy(r => r.Category)
        .ThenBy(r => r.Name);

    return query;
  }

  public RecipeEntity? Get(int id)
  {
    return db.Recipes.Find(id);
  }

  public RecipeEntity Update(RecipeEntity entity)
  {
    var existing = db.Recipes.Attach(entity);
    existing.State = EntityState.Modified;
    return entity;
  }

  public IEnumerable<RecipeEntity> GetRecipesForUser(ClaimsPrincipal user)
  {
    return Find(r => r.UserId == user.GetNameIdentifier());
  }
}

