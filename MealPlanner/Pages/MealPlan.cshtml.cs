using MealPlanner.Core;
using MealPlanner.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MealPlanner.Pages.Recipes
{
    public class MealPlanModel : PageModel
    {
        private readonly IRecipeRepository recipeData;
        private readonly IMealPlanGenerator mealPlanGenerator;

        public MealPlan MealPlan { get; private set; }

        public MealPlanModel(IRecipeRepository recipeData, IMealPlanGenerator mealPlanGenerator)
        {
            this.recipeData = recipeData;
            this.mealPlanGenerator = mealPlanGenerator;
        }

        public void OnGet()
        {
            MealPlan = mealPlanGenerator.Generate(recipeData.All());
        }
    }
}
