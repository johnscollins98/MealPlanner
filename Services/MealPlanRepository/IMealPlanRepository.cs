using System.Security.Claims;

namespace MealPlanner;
public interface IMealPlanRepository : IRepository<MealPlanEntity>
{
  public MealPlanEntity? GetMealPlanForUser(ClaimsPrincipal user);
}
