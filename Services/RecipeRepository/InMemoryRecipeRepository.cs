using System.Linq.Expressions;
using System.Security.Claims;

namespace MealPlanner;
public class InMemoryRecipeRepository : IRecipeRepository
{
  public List<RecipeEntity> Recipes { get; set; }

  public InMemoryRecipeRepository()
  {
    Recipes = new List<RecipeEntity>
            {
                new RecipeEntity
                {
                    ID = 1,
                    Name = "Soup",
                    Category = MealCategory.Vegetarian,
                    Time = MealTime.Lunch
                },
                new RecipeEntity
                {
                    ID = 2,
                    Name = "Burger",
                    Category = MealCategory.Meat,
                    Time = MealTime.Dinner
                },
                new RecipeEntity
                {
                    ID = 3,
                    Name = "Hot Dog",
                    Category = MealCategory.Meat,
                    Time = MealTime.Dinner
                },
                new RecipeEntity
                {
                    ID = 4,
                    Name = "Chicken Wraps",
                    Category = MealCategory.Poultry,
                    Time = MealTime.Dinner
                },
                new RecipeEntity
                {
                    ID = 5,
                    Name = "Stuffed Peppers",
                    Category = MealCategory.Vegetarian,
                    Time = MealTime.Dinner
                },

                new RecipeEntity
                {
                    ID = 6,
                    Name = "Cereal",
                    Category = MealCategory.Vegetarian,
                    Time = MealTime.Breakfast
                },
            };
  }

  public RecipeEntity Add(RecipeEntity recipe)
  {
    recipe.ID = Recipes.Count() > 0 ? Recipes.Max(r => r.ID) + 1 : 1;
    Recipes.Add(recipe);
    return recipe;
  }

  public int Commit()
  {
    return 0;
  }

  public RecipeEntity? Delete(int id)
  {
    var recipe = Recipes.FirstOrDefault(r => r.ID == id);
    if (recipe != null)
    {
      Recipes.Remove(recipe);
    }
    return recipe;
  }

  public RecipeEntity? Get(int id)
  {
    return Recipes.FirstOrDefault(r => r.ID == id);
  }

  public RecipeEntity? Update(RecipeEntity newRecipe)
  {
    var recipe = Recipes.SingleOrDefault(r => r.ID == newRecipe.ID);
    if (recipe != null)
    {
      recipe.Name = newRecipe.Name;
      recipe.Time = newRecipe.Time;
      recipe.Category = newRecipe.Category;
    }
    return recipe;
  }

  public IEnumerable<RecipeEntity> Find(Expression<Func<RecipeEntity, bool>> predicate)
  {
    return Recipes
        .AsQueryable()
        .Where(predicate)
        .ToList();
  }

  public IEnumerable<RecipeEntity> All()
  {
    return Recipes;
  }

  public IEnumerable<RecipeEntity> GetRecipesForUser(ClaimsPrincipal user)
  {
    return Recipes
        .Where(r => r.UserId == user.GetNameIdentifier())
        .ToList();
  }
}
