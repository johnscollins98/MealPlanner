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
    public class MealPlanModel : PageModel
    {
        private readonly IRecipeData recipeData;

        public IEnumerable<Recipe> Recipes { get; set; }
        public IEnumerable<Recipe> Breakfasts { get; set; }
        public IEnumerable<Recipe> Lunches { get; set; }
        public IEnumerable<Recipe> Dinners { get; set; }
        public IEnumerable<Recipe> Snacks { get; set; }

        public MealPlanModel(IRecipeData recipeData)
        {
            this.recipeData = recipeData;
        }

        public void OnGet()
        {
            Recipes = recipeData.GetRecipes(null, null, null);
            Breakfasts = GetBreakfasts();
            Lunches = GetLunches();
            Dinners = GetDinners();
        }

        private IEnumerable<Recipe> GetBreakfasts()
        {
            var query = from r in Recipes
                        where r.Time == MealTime.Breakfast
                        select r;

            return query.OrderBy(r => Guid.NewGuid()).Take(2);
        }

        private IEnumerable<Recipe> GetLunches()
        {
            var query = from r in Recipes
                        where r.Time == MealTime.Lunch
                        select r;

            return query.OrderBy(r => Guid.NewGuid()).Take(2);
        }

        private IEnumerable<Recipe> GetDinners()
        {
            var query = from r in Recipes
                        where r.Time == MealTime.Dinner
                        select r;

            return query.OrderBy(r => Guid.NewGuid()).Take(2);
        }
    }
}
