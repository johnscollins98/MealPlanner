using MealPlanner.Core;
using MealPlanner.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace MealPlanner.Pages
{
    [Authorize]
    public class IndexModel : PageModel
    {
        public string Name { get; set; }

        public IndexModel(IRecipeRepository recipeData, IHtmlHelper htmlHelper)
        {
        }

        public void OnGet()
        {
            Name = User.Identity.Name.Split(' ')[0];
        }
    }
}
