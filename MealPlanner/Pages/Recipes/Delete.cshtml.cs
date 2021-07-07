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
    public class DeleteModel : PageModel
    {
        private readonly IRecipeData recipeData;

        public Recipe Recipe { get; private set; }

        public DeleteModel(IRecipeData recipeData)
        {
            this.recipeData = recipeData;
        }

        public IActionResult OnGet(int recipeId)
        {
            Recipe = recipeData.GetRecipe(recipeId);
            if (Recipe == null)
            {
                return RedirectToPage("./NotFound");
            }
            return Page();
        }

        public IActionResult OnPost(int recipeId)
        {
            var recipe = recipeData.Delete(recipeId);
            if (recipe == null)
            {
                return RedirectToPage("./NotFound");
            }
            return RedirectToPage("Index");
        }
    }
}
