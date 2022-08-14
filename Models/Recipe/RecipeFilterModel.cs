namespace MealPlanner
{
  public class RecipeFilterModel
  {
    public MealCategory? CategoryFilter { get; set; }

    public MealTime? TimeFilter { get; set; }

    public string NameFilter { get; set; }

    public int? CalorieFilter { get; set; }

    public string BookTitle { get; set; }
  }
}