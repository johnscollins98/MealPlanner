using MealPlanner.Core;
using System.Security.Claims;

namespace MealPlanner.Data.MealPlanRepository
{
    public interface IMealPlanRepository : IRepository<MealPlan>
    {
        public MealPlan GetMealPlanForUser(ClaimsPrincipal user) ;
    }
}
