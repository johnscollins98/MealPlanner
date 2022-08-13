using MealPlanner.Core;

namespace MealPlanner.Pages.Shared
{
  public class RecipeCardModel
  {
    public Recipe Recipe { get; set; }
    public bool ShowActions { get; set; } = false;
  }
}