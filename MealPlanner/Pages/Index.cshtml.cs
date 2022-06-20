using MealPlanner.Core;
using MealPlanner.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace MealPlanner.Pages.Recipes
{
    public class IndexModel : PageModel
    {
        private readonly IRecipeRepository recipeData;
        private readonly IHtmlHelper htmlHelper;

        public IEnumerable<Recipe> Recipes { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }

        public IEnumerable<SelectListItem> Times { get; set; }

        [BindProperty(SupportsGet = true)]
        public MealCategory? CategoryFilter { get; set; }

        [BindProperty(SupportsGet = true)]
        public MealTime? TimeFilter { get; set; }

        [BindProperty(SupportsGet = true)]
        public string NameFilter { get; set; }

        [BindProperty(SupportsGet = true)]
        public int CalorieFilter { get; set; }

        public IndexModel(IRecipeRepository recipeData, IHtmlHelper htmlHelper)
        {
            this.recipeData = recipeData;
            this.htmlHelper = htmlHelper;
        }

        public void OnGet()
        {
            Categories = htmlHelper.GetEnumSelectList<MealCategory>();
            Times = htmlHelper.GetEnumSelectList<MealTime>();

            Recipes = recipeData.Find(r =>
                (CategoryFilter == null || r.Category == CategoryFilter)
                && (TimeFilter == null || r.Time == TimeFilter)
                && (string.IsNullOrEmpty(NameFilter) || r.Name.ToLower().Contains(NameFilter.ToLower()))
                && (CalorieFilter == 0 || r.Calories <= CalorieFilter)
            );
        }
    }
}
