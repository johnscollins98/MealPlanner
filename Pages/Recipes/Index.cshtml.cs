using MealPlanner.Core;
using MealPlanner.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace MealPlanner.Pages.Recipes
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IRecipeRepository recipeData;

        public IEnumerable<Recipe> Recipes { get; set; }

        [BindProperty(SupportsGet = true)]
        public MealCategory? CategoryFilter { get; set; }

        [BindProperty(SupportsGet = true)]
        public MealTime? TimeFilter { get; set; }

        [BindProperty(SupportsGet = true)]
        public string NameFilter { get; set; }

        [BindProperty(SupportsGet = true)]
        public int CalorieFilter { get; set; }

        [BindProperty(SupportsGet = true)]
        public string BookTitle { get; set; }

        public IndexModel(IRecipeRepository recipeData, IHtmlHelper htmlHelper)
        {
            this.recipeData = recipeData;
        }

        public void OnGet()
        {
            var userId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            Recipes = recipeData.Find(r =>
                r.UserId == userId
                && (CategoryFilter == null || r.Category == CategoryFilter)
                && (TimeFilter == null || r.Time == TimeFilter)
                && (string.IsNullOrEmpty(NameFilter) || r.Name.ToLower().Contains(NameFilter.ToLower()))
                && (string.IsNullOrEmpty(BookTitle) || r.BookTitle.ToLower().Contains(BookTitle.ToLower()))
                && (CalorieFilter == 0 || r.Calories <= CalorieFilter)
            );
        }
    }
}
