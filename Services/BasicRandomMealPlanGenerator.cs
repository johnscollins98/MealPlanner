namespace MealPlanner;

public class BasicRandomMealPlanGenerator : IMealPlanGenerator
{
  public BasicRandomMealPlanGenerator()
  {
  }

  public MealPlanEntity Generate(IEnumerable<RecipeEntity> recipes)
  {
    var breakfasts = GetByMealTime(MealTime.Breakfast, recipes);
    var lunches = GetByMealTime(MealTime.Lunch, recipes);
    var dinners = GetByMealTime(MealTime.Dinner, recipes);
    var snacks = GetByMealTime(MealTime.Snack, recipes);

    var chosenBreakfasts = breakfasts.Take(2);
    var chosenLunches = new List<RecipeEntity?>
    {
      lunches.FirstOrDefault(r => r.Category == MealCategory.Meat || r.Category == MealCategory.Poultry),
      lunches.FirstOrDefault(r => r.Category == MealCategory.Vegetarian)
    };

    var chosenDinners = new List<RecipeEntity?>
    {
      dinners.FirstOrDefault(r => r.Category == MealCategory.Meat),
      dinners.FirstOrDefault(r => r.Category == MealCategory.Poultry),
      dinners.FirstOrDefault(r => r.Category == MealCategory.Vegetarian),
    };

    var chosenSnacks = new List<RecipeEntity?>
    {
      snacks.FirstOrDefault(r => r.Category == MealCategory.Sweet),
      snacks.FirstOrDefault(r => r.Category == MealCategory.Savoury),
    };

    var plan = new MealPlanEntity
    {
      Recipes = chosenBreakfasts.OfType<RecipeEntity>()
        .Concat(chosenLunches.OfType<RecipeEntity>())
        .Concat(chosenDinners.OfType<RecipeEntity>())
        .Concat(chosenSnacks.OfType<RecipeEntity>())
        .ToList()
    };

    return plan;
  }

  private IEnumerable<RecipeEntity> GetByMealTime(MealTime time, IEnumerable<RecipeEntity> recipes)
  {
    var query = recipes
        .Where(r => r.Time == time)
        .OrderBy(r => Guid.NewGuid());

    return query;
  }
}
