namespace MealPlanner
{
  public class RecipeFilterModel
  {
    public MealCategory? Category { get; set; }

    public MealTime? Time { get; set; }

    public string Name { get; set; }

    public int? Calorie { get; set; }

    public string BookTitle { get; set; }
  }
}