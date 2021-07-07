using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MealPlanner.Core;
using MealPlanner.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MealPlanner.Pages.Recipes
{
    public class MealPlanModel : PageModel
    {
        private readonly IRecipeData recipeData;
        private readonly IMealPlanGenerator mealPlanGenerator;

        public MealPlan MealPlan { get; private set; }

        public MealPlanModel(IRecipeData recipeData, IMealPlanGenerator mealPlanGenerator)
        {
            this.recipeData = recipeData;
            this.mealPlanGenerator = mealPlanGenerator;
        }

        public void OnGet()
        {
            MealPlan = mealPlanGenerator.Generate(recipeData.GetRecipes());
        }
    }
}
