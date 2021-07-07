using System.Collections.Generic;

namespace MealPlanner.Core
{
  public interface IMealPlanGenerator
  {
    MealPlan Generate(IEnumerable<Recipe> recipes);
  }
}