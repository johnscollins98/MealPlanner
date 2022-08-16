using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MealPlanner;
[Authorize]
public class DetailsModel : PageModel
{
  private readonly IRecipeRepository recipeData;
  private readonly IMapper mapper;

  public RecipeDetailsDto Recipe { get; set; } = new RecipeDetailsDto();

  public DetailsModel(IRecipeRepository recipeData, IMapper mapper)
  {
    this.recipeData = recipeData;
    this.mapper = mapper;
  }

  public IActionResult OnGet(int recipeId)
  {
    var userId = User.GetNameIdentifier();
    var recipe = recipeData.Find(r =>
        r.ID == recipeId && r.UserId == userId
    ).FirstOrDefault();

    if (recipe == null)
    {
      return RedirectToPage("/NotFound");
    }

    Recipe = mapper.Map<RecipeDetailsDto>(recipe);

    return Page();
  }
}
