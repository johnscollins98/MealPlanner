using System.Collections.Generic;
using System.Security.Claims;
using MealPlanner.Core;

namespace MealPlanner.Data
{
    public interface IRecipeRepository : IRepository<Recipe> 
    { 
        public IEnumerable<Recipe> GetRecipesForUser(ClaimsPrincipal user);
    }
}
