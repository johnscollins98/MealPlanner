using System.Collections.Generic;
using MealPlanner.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MealPlanner.Pages.Recipes
{
  public class FilterModel : PageModel
  {
    private readonly IHtmlHelper htmlHelper;

    public IEnumerable<SelectListItem> Categories { get; set; }

    public IEnumerable<SelectListItem> Times { get; set; }

    public RecipeFilterModel Filter { get; set; }

    public FilterModel(IHtmlHelper htmlHelper)
    {
      this.htmlHelper = htmlHelper ?? throw new System.ArgumentNullException(nameof(htmlHelper));
    }

    public void OnGet()
    {
      Categories = htmlHelper.GetEnumSelectList<MealCategory>();
      Times = htmlHelper.GetEnumSelectList<MealTime>();
    }
  }
}