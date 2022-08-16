using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MealPlanner;
[Authorize]
public class DeleteModel : PageModel
{
  private readonly IRecipeRepository recipeData;

  public string RecipeName { get; private set; } = String.Empty;

  public DeleteModel(IRecipeRepository recipeData)
  {
    this.recipeData = recipeData;
  }

  public IActionResult OnGet(int recipeId)
  {
    var userId = User.GetNameIdentifier();

    var recipe = recipeData.Find(recipe =>
        recipe.UserId == userId && recipe.ID == recipeId
    ).FirstOrDefault();
    if (recipe == null)
    {
      return RedirectToPage("./NotFound");
    }

    RecipeName = recipe.Name;

    return Page();
  }

  public IActionResult OnPost(int recipeId)
  {
    var recipe = recipeData.Delete(recipeId);
    if (recipe == null || recipe.UserId != User.GetNameIdentifier())
    {
      return RedirectToPage("/NotFound");
    }
    TempData["Message"] = $"Deleted recipe";
    recipeData.Commit();
    return RedirectToPage("Index");
  }
}
