using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MealPlanner.Recipes;
[Authorize]
public class EditModel : PageModel
{
  private readonly IRecipeRepository recipeData;
  private readonly IHtmlHelper htmlHelper;
  private readonly IMapper mapper;

  [BindProperty]
  public RecipeCreationDto Recipe { get; set; } = new RecipeCreationDto();

  [BindProperty(SupportsGet = true)]
  public int? RecipeId { get; set; }
  public IEnumerable<SelectListItem> Categories { get; private set; } = Enumerable.Empty<SelectListItem>();
  public IEnumerable<SelectListItem> Times { get; private set; } = Enumerable.Empty<SelectListItem>();

  public EditModel(IRecipeRepository recipeData, IHtmlHelper htmlHelper, IMapper mapper)
  {
    this.recipeData = recipeData;
    this.htmlHelper = htmlHelper;
    this.mapper = mapper;
  }

  public IActionResult OnGet()
  {
    Categories = htmlHelper.GetEnumSelectList<MealCategory>();
    Times = htmlHelper.GetEnumSelectList<MealTime>();

    var userId = User.GetNameIdentifier();
    if (String.IsNullOrEmpty(userId))
    {
      return NotFound();
    }

    if (RecipeId.HasValue)
    {
      var recipeEntity = recipeData.Find(r =>
          r.ID == RecipeId.Value && r.UserId == userId
      ).FirstOrDefault();
      Recipe = mapper.Map<RecipeCreationDto>(recipeEntity);
    }
    else
    {
      Recipe = new RecipeCreationDto();
    }

    if (Recipe == null)
    {
      return RedirectToPage("NotFound");
    }

    return Page();
  }

  public IActionResult OnPost()
  {
    var userId = User.GetNameIdentifier();
    if (!ModelState.IsValid || String.IsNullOrEmpty(userId))
    {
      Categories = htmlHelper.GetEnumSelectList<MealCategory>();
      Times = htmlHelper.GetEnumSelectList<MealTime>();
      return Page();
    }


    if (RecipeId.HasValue)
    {
      var recipeEntity = recipeData.Get(RecipeId.Value);
      if (recipeEntity == null)
      {
        return RedirectToPage("/NotFound");
      }

      // TODO - is there a better way to do this?
      recipeEntity.UserId = userId;
      recipeEntity.BookTitle = Recipe.BookTitle;
      recipeEntity.Calories = Recipe.Calories;
      recipeEntity.Category = Recipe.Category;
      recipeEntity.Name = Recipe.Name;
      recipeEntity.Notes = Recipe.Notes;
      recipeEntity.PageNumber = Recipe.PageNumber;
      recipeEntity.Time = Recipe.Time;
      recipeData.Update(recipeEntity);
      TempData["Message"] = $"Updated '{Recipe.Name}'";
    }
    else
    {
      var recipeEntity = mapper.Map<RecipeEntity>(Recipe);
      recipeEntity.UserId = userId;
      recipeData.Add(recipeEntity);
      TempData["Message"] = $"Created '{Recipe.Name}'";
    }
    recipeData.Commit();
    return RedirectToPage("/Recipes/Index", new { recipeId = RecipeId });
  }
}

