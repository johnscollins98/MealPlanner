using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MealPlanner.Recipes;
[Authorize]
public class IndexModel : PageModel
{
  private readonly IRecipeRepository recipeData;
  private readonly IMapper mapper;

  public IEnumerable<RecipeListEntryDto> Recipes { get; set; } = Enumerable.Empty<RecipeListEntryDto>();

  [BindProperty(SupportsGet = true)]
  public RecipeFilterModel Filter { get; set; } = new RecipeFilterModel();

  public IndexModel(IRecipeRepository recipeData, IHtmlHelper htmlHelper, IMapper mapper)
  {
    this.recipeData = recipeData;
    this.mapper = mapper;
  }

  public void OnGet()
  {
    var userId = User.GetNameIdentifier();
    var recipeEntities = recipeData.Find(r =>
        r.UserId == userId
        && (Filter.Category == null || r.Category == Filter.Category)
        && (Filter.Time == null || r.Time == Filter.Time)
        && (string.IsNullOrEmpty(Filter.Name) || r.Name.ToLower().Contains(Filter.Name.ToLower()))
        && (string.IsNullOrEmpty(Filter.BookTitle) 
            || !string.IsNullOrEmpty(r.BookTitle) 
            && r.BookTitle.ToLower().Contains(Filter.BookTitle.ToLower()))
        && (!Filter.Calorie.HasValue || r.Calories <= Filter.Calorie.Value)
    );
    Recipes = mapper.Map<IEnumerable<RecipeListEntryDto>>(recipeEntities);
  }
}
