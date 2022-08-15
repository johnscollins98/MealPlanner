using System;
using System.ComponentModel.DataAnnotations;

namespace MealPlanner
{
  public class RecipeCreationDto
  {
    [Required]
    public string Name { get; set; } = String.Empty;

    [Required]
    public MealCategory Category { get; set; }

    [Required]
    public MealTime Time { get; set; }

    public int? Calories { get; set; }

    public string? BookTitle { get; set; } = String.Empty;

    public int? PageNumber { get; set; }

    public string? Notes { get; set; } = String.Empty;
  }
}