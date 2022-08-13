using MealPlanner.Core;
using MealPlanner.Data;
using MealPlanner.Data.MealPlanRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace MealPlanner.Pages.MealPlan
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IRecipeRepository recipeData;
        private readonly IMealPlanGenerator mealPlanGenerator;
        private readonly IMealPlanRepository mealPlanRepository;

        public IEnumerable<Recipe> Breakfasts { get; private set; }
        public IEnumerable<Recipe> Lunches { get; private set; }
        public IEnumerable<Recipe> Dinners { get; private set; }
        public IEnumerable<Recipe> Snacks { get; private set; }

        public IndexModel(IRecipeRepository recipeData, IMealPlanGenerator mealPlanGenerator, IMealPlanRepository mealPlanRepository)
        {
            this.recipeData = recipeData;
            this.mealPlanGenerator = mealPlanGenerator;
            this.mealPlanRepository = mealPlanRepository;
        }

        public void OnGet()
        {
            var mealPlan = mealPlanRepository.All().FirstOrDefault();
            
            if (mealPlan == null)
            {
                mealPlan = mealPlanRepository.Add(mealPlanGenerator.Generate(recipeData.All()));
                mealPlanRepository.Commit();
            }

            Breakfasts = mealPlan.Recipes.Where(r => r.Time == MealTime.Breakfast);
            Lunches = mealPlan.Recipes.Where(r => r.Time == MealTime.Lunch);
            Dinners = mealPlan.Recipes.Where(r => r.Time == MealTime.Dinner);
            Snacks = mealPlan.Recipes.Where(r => r.Time == MealTime.Snack);
        }

        public IActionResult OnPost()
        {
            var existingMealPlan = mealPlanRepository.All().FirstOrDefault();
            var userId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var recipes = recipeData.Find(recipe => 
                recipe.UserId == userId
            );
            var newMealPlan = mealPlanGenerator.Generate(recipes);
            if (existingMealPlan != null)
            {
                existingMealPlan.Recipes = newMealPlan.Recipes;
                mealPlanRepository.Update(existingMealPlan);
            }
            else
            {
                mealPlanRepository.Add(newMealPlan);
            }
            mealPlanRepository.Commit();

            return Redirect("./MealPlan");
        }
    }
}
