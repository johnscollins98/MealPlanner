using MealPlanner.Core;
using MealPlanner.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace MealPlanner.Pages.Recipes
{
    [Authorize]
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

            var userId = User.GetNameIdentifier();
            if (String.IsNullOrEmpty(userId))
            {
                return NotFound();
            }

            if (recipeId.HasValue)
            {
                Recipe = recipeData.Find(r => 
                    r.ID == recipeId.Value && r.UserId == userId 
                ).FirstOrDefault();
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
            var userId = User.GetNameIdentifier();
            if (!ModelState.IsValid || String.IsNullOrEmpty(userId))
            {
                Categories = htmlHelper.GetEnumSelectList<MealCategory>();
                Times = htmlHelper.GetEnumSelectList<MealTime>();
                return Page();
            }

            Recipe.UserId = userId;

            if (Recipe.ID > 0)
            {
                recipeData.Update(Recipe);
                TempData["Message"] = $"Updated '{Recipe.Name}'";
            }
            else
            {
                recipeData.Add(Recipe);
                TempData["Message"] = $"Created '{Recipe.Name}'";
            }
            recipeData.Commit();
            return RedirectToPage("./Index", new { recipeId = Recipe.ID });
        }
    }
}
