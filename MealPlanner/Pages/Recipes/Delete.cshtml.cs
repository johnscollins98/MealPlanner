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
        private readonly IRecipeRepository recipeData;

        public Recipe Recipe { get; private set; }

        public DeleteModel(IRecipeRepository recipeData)
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

        public IActionResult OnPost(int recipeId)
        {
            var recipe = recipeData.Delete(recipeId);
            if (recipe == null)
            {
                return RedirectToPage("./NotFound");
            }
            recipeData.Commit();
            return RedirectToPage("Index");
        }
    }
}
