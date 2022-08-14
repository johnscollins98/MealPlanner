using MealPlanner.Core;
using MealPlanner.Data;
using MealPlanner.Data.MealPlanRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace MealPlanner.Pages.MealPlan
{
  [Authorize]
  public class EditModel : PageModel
  {
    private readonly IRecipeRepository recipeData;
    private readonly IMealPlanRepository mealPlanRepository;
    public IEnumerable<SelectListItem> AllRecipes { get; set; }
    public IList<string> RecipeIds { get; private set; }

    public EditModel(IRecipeRepository recipeData,
      IMealPlanRepository mealPlanRepository)
    {
      this.recipeData = recipeData;
      this.mealPlanRepository = mealPlanRepository;
    }

    public void OnGet()
    {
      var userId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
      var mealPlan = mealPlanRepository
        .Find(mp => mp.UserId == userId)
        .FirstOrDefault();

      AllRecipes = recipeData.Find(r => r.UserId == userId)
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
    }

    public IActionResult OnPost([FromForm] IEnumerable<string> recipeIds)
    {
      var userId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
      var existingMealPlan = mealPlanRepository
        .Find(mp => mp.UserId == userId)
        .FirstOrDefault();

      if (existingMealPlan == null)
      {
        return NotFound();
      }

      List<Recipe> newRecipes = recipeIds
        .Select(id =>
        {
          var idAsInt = Int32.Parse(id);
          var recipe = recipeData.Find(r => r.ID == idAsInt).First();
          return recipe;
        }).ToList();

      existingMealPlan.Recipes = newRecipes;
      mealPlanRepository.Update(existingMealPlan);
      mealPlanRepository.Commit();

      return Redirect("/MealPlan/Index");
    }
  }
}
