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
            var userId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var mealPlan = mealPlanRepository
                .Find(mp => mp.UserId == userId)
                .FirstOrDefault();
            
            if (mealPlan == null)
            {
                var recipes = recipeData.Find(recipe => 
                    recipe.UserId == userId
                );
                var mealPlanToCreate = mealPlanGenerator.Generate(recipes);
                mealPlanToCreate.UserId = userId;
                mealPlan = mealPlanRepository.Add(mealPlanToCreate);
                mealPlanRepository.Commit();
            }

            Breakfasts = mealPlan.Recipes.Where(r => r.Time == MealTime.Breakfast);
            Lunches = mealPlan.Recipes.Where(r => r.Time == MealTime.Lunch);
            Dinners = mealPlan.Recipes.Where(r => r.Time == MealTime.Dinner);
            Snacks = mealPlan.Recipes.Where(r => r.Time == MealTime.Snack);
        }

        public IActionResult OnPost()
        {
            var userId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var existingMealPlan = mealPlanRepository
                .Find(mp => mp.UserId == userId)
                .FirstOrDefault();

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
                newMealPlan.UserId = userId;
                mealPlanRepository.Add(newMealPlan);
            }
            mealPlanRepository.Commit();

            return Redirect("./MealPlan");
        }
    }
}
