using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MealPlanner.MealPlan;
[Authorize]
public class IndexModel : PageModel
{
  private readonly IRecipeRepository recipeData;
  private readonly IMapper mapper;
  private readonly IMealPlanGenerator mealPlanGenerator;
  private readonly IMealPlanRepository mealPlanRepository;

  public IEnumerable<RecipeListEntryDto> Breakfasts { get; private set; } = Enumerable.Empty<RecipeListEntryDto>();
  public IEnumerable<RecipeListEntryDto> Lunches { get; private set; } = Enumerable.Empty<RecipeListEntryDto>();
  public IEnumerable<RecipeListEntryDto> Dinners { get; private set; } = Enumerable.Empty<RecipeListEntryDto>();
  public IEnumerable<RecipeListEntryDto> Snacks { get; private set; } = Enumerable.Empty<RecipeListEntryDto>();

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
    if (User == null)
    {
      throw new NoUserException();
    }

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
    if (User == null)
    {
      throw new NoUserException();
    }

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
