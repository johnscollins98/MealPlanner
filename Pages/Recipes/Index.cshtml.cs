using AutoMapper;
using MealPlanner.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace MealPlanner.Pages.Recipes
{
  [Authorize]
  public class IndexModel : PageModel
  {
    private readonly IRecipeRepository recipeData;
    private readonly IMapper mapper;

    public IEnumerable<RecipeListEntryDto> Recipes { get; set; }

    [BindProperty(SupportsGet = true)]
    public RecipeFilterModel FilterData { get; set; }

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
          && (FilterData.CategoryFilter == null || r.Category == FilterData.CategoryFilter)
          && (FilterData.TimeFilter == null || r.Time == FilterData.TimeFilter)
          && (string.IsNullOrEmpty(FilterData.NameFilter) || r.Name.ToLower().Contains(FilterData.NameFilter.ToLower()))
          && (string.IsNullOrEmpty(FilterData.BookTitle) || r.BookTitle.ToLower().Contains(FilterData.BookTitle.ToLower()))
          && (!FilterData.CalorieFilter.HasValue || r.Calories <= FilterData.CalorieFilter.Value)
      );
      Recipes = mapper.Map<IEnumerable<RecipeListEntryDto>>(recipeEntities);
    }
  }
}
