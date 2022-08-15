namespace MealPlanner;
public class MealPlanEntity
{
  public int Id { get; set; }
  public string UserId { get; set; } = String.Empty;
  public ICollection<RecipeEntity> Recipes { get; set; } = new List<RecipeEntity>();
}