using System;

namespace MealPlanner
{
  public class RecipeDetailsDto
  {
    public string Name { get; set; }

    public MealCategory Category { get; set; }

    public MealTime Time { get; set; }

    public int Calories { get; set; }

    public string BookTitle { get; set; } = String.Empty;

    public int PageNumber { get; set; }

    public string Notes { get; set; }
  }
}