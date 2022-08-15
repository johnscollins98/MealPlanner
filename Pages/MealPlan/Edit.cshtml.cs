using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MealPlanner.MealPlan;
[Authorize]
public class EditModel : PageModel
{
  private readonly IRecipeRepository recipeData;
  private readonly IMealPlanRepository mealPlanRepository;
  public IEnumerable<SelectListItem> AllRecipes { get; set; } = Enumerable.Empty<SelectListItem>();
  public IEnumerable<string> RecipeIds { get; private set; } = Enumerable.Empty<string>();

  public EditModel(IRecipeRepository recipeData,
    IMealPlanRepository mealPlanRepository)
  {
    this.recipeData = recipeData;
    this.mealPlanRepository = mealPlanRepository;
  }

  public IActionResult OnGet()
  {
    var mealPlan = mealPlanRepository.GetMealPlanForUser(User);
    if (mealPlan == null)
    {
      return RedirectToPage("/NotFound");
    }

    AllRecipes = recipeData.GetRecipesForUser(User)
      .Select(recipe =>
      {
        return new SelectListItem
        {
          Value = recipe.ID.ToString(),
          Text = recipe.Name
        };
      }).ToList();

    RecipeIds = mealPlan.Recipes
      .Select(mp => mp.ID.ToString())
      .ToList();
    
    return Page();
  }

  public IActionResult OnPost([FromForm] IEnumerable<string> recipeIds)
  {
    var existingMealPlan = mealPlanRepository.GetMealPlanForUser(User);
    if (existingMealPlan == null)
    {
      return RedirectToPage("/NotFound");
    }

    List<RecipeEntity> newRecipes = recipeIds
      .Select(id =>
      {
        var idAsInt = Int32.Parse(id);
        var recipe = recipeData.Find(r => r.ID == idAsInt).First();
        return recipe;
      }).ToList();

    existingMealPlan.Recipes = newRecipes;
    mealPlanRepository.Update(existingMealPlan);
    mealPlanRepository.Commit();

    return RedirectToPage("/MealPlan/Index");
  }
}
