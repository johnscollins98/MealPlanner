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
    public class IndexModel : PageModel
    {
        private readonly IRecipeData recipeData;

        public IEnumerable<Recipe> Recipes { get; set; }

        public IndexModel(IRecipeData recipeData)
        {
            this.recipeData = recipeData;
        }

        public void OnGet()
        {
            Recipes = recipeData.GetRecipes(null, null, null);
        }
    }
}
