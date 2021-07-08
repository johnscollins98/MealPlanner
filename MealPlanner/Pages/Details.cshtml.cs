using MealPlanner.Core;
using MealPlanner.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MealPlanner.Pages.Recipes
{
    public class DetailsModel : PageModel
    {
        private readonly IRecipeRepository recipeData;

        public Recipe Recipe { get; set; }

        public DetailsModel(IRecipeRepository recipeData)
        {
            this.recipeData = recipeData;
        }

        public IActionResult OnGet(int recipeId)
        {
            Recipe = recipeData.Get(recipeId);
            if (Recipe == null)
            {
                return RedirectToPage("./NotFound");
            }
            return Page();
        }
    }
}
