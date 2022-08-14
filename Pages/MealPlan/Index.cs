using AutoMapper;
using MealPlanner.Core;
using MealPlanner.Data;
using MealPlanner.Data.MealPlanRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;

namespace MealPlanner.Pages.MealPlan
{
  [Authorize]
  public class IndexModel : PageModel
  {
    private readonly IRecipeRepository recipeData;
    private readonly IMapper mapper;
    private readonly IMealPlanGenerator mealPlanGenerator;
    private readonly IMealPlanRepository mealPlanRepository;

    public IEnumerable<RecipeListEntryDto> Breakfasts { get; private set; }
    public IEnumerable<RecipeListEntryDto> Lunches { get; private set; }
    public IEnumerable<RecipeListEntryDto> Dinners { get; private set; }
    public IEnumerable<RecipeListEntryDto> Snacks { get; private set; }

    public IndexModel(IRecipeRepository recipeData, IMapper mapper,
        IMealPlanGenerator mealPlanGenerator, IMealPlanRepository mealPlanRepository)
    {
      this.recipeData = recipeData;
      this.mapper = mapper;
      this.mealPlanGenerator = mealPlanGenerator;
      this.mealPlanRepository = mealPlanRepository;
    }

    public void OnGet()
    {
      var mealPlan = mealPlanRepository.GetMealPlanForUser(User);

      if (mealPlan == null)
      {
        var recipes = recipeData.GetRecipesForUser(User);
        var mealPlanToCreate = mealPlanGenerator.Generate(recipes);
        mealPlanToCreate.UserId = User.GetNameIdentifier();
        mealPlan = mealPlanRepository.Add(mealPlanToCreate);
        mealPlanRepository.Commit();
      }

      var recipeDtos = mapper.Map<IEnumerable<RecipeListEntryDto>>(mealPlan.Recipes)
        .Select(r =>
        {
            r.ID = null; // make ID null so actions don't show
            return r;
        });

      Breakfasts = recipeDtos.Where(r => r.Time == MealTime.Breakfast);
      Lunches = recipeDtos.Where(r => r.Time == MealTime.Lunch);
      Dinners = recipeDtos.Where(r => r.Time == MealTime.Dinner);
      Snacks = recipeDtos.Where(r => r.Time == MealTime.Snack);
    }

    public IActionResult OnPost()
    {
      var existingMealPlan = mealPlanRepository.GetMealPlanForUser(User);
      var recipes = recipeData.GetRecipesForUser(User);

      var newMealPlan = mealPlanGenerator.Generate(recipes);
      if (existingMealPlan != null)
      {
        existingMealPlan.Recipes = newMealPlan.Recipes;
        mealPlanRepository.Update(existingMealPlan);
      }
      else
      {
        newMealPlan.UserId = User.GetNameIdentifier();
        mealPlanRepository.Add(newMealPlan);
      }
      mealPlanRepository.Commit();

      return Redirect("/MealPlan/Index");
    }
  }
}
