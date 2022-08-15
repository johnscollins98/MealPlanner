namespace MealPlanner
{
  public class RecipeFilterModel
  {
    public MealCategory? Category { get; set; }

    public MealTime? Time { get; set; }

    public string Name { get; set; } = String.Empty;

    public int? Calorie { get; set; }

    public string BookTitle { get; set; } = String.Empty;
  }
}