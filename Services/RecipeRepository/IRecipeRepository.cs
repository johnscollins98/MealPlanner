using System.Security.Claims;

namespace MealPlanner;
public interface IRecipeRepository : IRepository<RecipeEntity>
{
  public IEnumerable<RecipeEntity> GetRecipesForUser(ClaimsPrincipal user);
}
