namespace MealPlanner;
public interface IMealPlanGenerator
{
  MealPlanEntity Generate(IEnumerable<RecipeEntity> recipes);
}