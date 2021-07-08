using MealPlanner.Core;
using MealPlanner.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace MealPlanner.Pages.Recipes
{
    public class IndexModel : PageModel
    {
        private readonly IRecipeRepository recipeData;

        public IEnumerable<Recipe> Recipes { get; set; }

        public IndexModel(IRecipeRepository recipeData)
        {
            this.recipeData = recipeData;
        }

        public void OnGet()
        {
            Recipes = recipeData.All();
        }
    }
}
