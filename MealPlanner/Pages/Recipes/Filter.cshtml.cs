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

    [BindProperty]
    public MealCategory? CategoryFilter { get; set; }

    [BindProperty]
    public MealTime? TimeFilter { get; set; }

    [BindProperty]
    public string NameFilter { get; set; }

    [BindProperty]
    public int CalorieFilter { get; set; }

    public string BookTitle { get; set; }

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