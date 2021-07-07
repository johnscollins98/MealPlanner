using System.Collections.Generic;

namespace MealPlanner.Core
{
  public class MealPlan
  {
    public IEnumerable<Recipe> Breakfasts { get; set; }
    public IEnumerable<Recipe> Lunches { get; set; }
    public IEnumerable<Recipe> Dinners { get; set; }
    public IEnumerable<Recipe> Snacks { get; set; }
  }
}