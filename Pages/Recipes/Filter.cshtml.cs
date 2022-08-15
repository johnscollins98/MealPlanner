using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MealPlanner;
public class FilterModel : PageModel
{
  private readonly IHtmlHelper htmlHelper;

  public IEnumerable<SelectListItem> Categories { get; set; } = Enumerable.Empty<SelectListItem>();

  public IEnumerable<SelectListItem> Times { get; set; } = Enumerable.Empty<SelectListItem>();

  public RecipeFilterModel Filter { get; set; } = new RecipeFilterModel();

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