using System.ComponentModel.DataAnnotations;

namespace MealPlanner;
public class RecipeEntity
{
  public int ID { get; set; }

  public string UserId { get; set; } = String.Empty;

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

  public ICollection<MealPlanEntity> MealPlans { get; set; } = new List<MealPlanEntity>();
}
