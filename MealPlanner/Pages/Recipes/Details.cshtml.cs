using System.Linq;
using System.Security.Claims;
using MealPlanner.Core;
using MealPlanner.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MealPlanner.Pages.Recipes
{
    [Authorize]
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
            var userId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            Recipe = recipeData.Get(recipeId);
            Recipe = recipeData.Find(r => 
                r.ID == recipeId && r.UserId == userId 
            ).FirstOrDefault();
            if (Recipe == null)
            {
                return RedirectToPage("./NotFound");
            }
            return Page();
        }
    }
}
