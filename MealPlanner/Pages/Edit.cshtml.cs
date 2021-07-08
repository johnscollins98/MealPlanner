using MealPlanner.Core;
using MealPlanner.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace MealPlanner.Pages.Recipes
{
    public class EditModel : PageModel
    {
        private readonly IRecipeRepository recipeData;
        private readonly IHtmlHelper htmlHelper;

        [BindProperty]
        public Recipe Recipe { get; set; }

        public EditModel(IRecipeRepository recipeData, IHtmlHelper htmlHelper)
        {
            this.recipeData = recipeData;
            this.htmlHelper = htmlHelper;
        }

        public IEnumerable<SelectListItem> Categories { get; private set; }
        public IEnumerable<SelectListItem> Times { get; private set; }

        public IActionResult OnGet(int? recipeId)
        {
            Categories = htmlHelper.GetEnumSelectList<MealCategory>();
            Times = htmlHelper.GetEnumSelectList<MealTime>();

            if (recipeId.HasValue)
            {
                Recipe = recipeData.Get(recipeId.Value);
            }
            else
            {
                Recipe = new Recipe();
            }

            if (Recipe == null)
            {
                return RedirectToPage("NotFound");
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                Categories = htmlHelper.GetEnumSelectList<MealCategory>();
                Times = htmlHelper.GetEnumSelectList<MealTime>();
                return Page();
            }

            if (Recipe.ID > 0)
            {
                recipeData.Update(Recipe);
            }
            else
            {
                recipeData.Add(Recipe);
            }
            recipeData.Commit();
            return RedirectToPage("./Details", new { recipeId = Recipe.ID });
        }
    }
}