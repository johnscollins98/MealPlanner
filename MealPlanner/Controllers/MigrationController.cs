using MealPlanner.Data;
using Microsoft.AspNetCore.Mvc;

namespace MealPlanner.Controllers.Recipes 
{
  [ApiController]
  [Route("[controller]")]
  public class MigrationController : ControllerBase
  {
    private readonly MealPlannerDbContext sqlDbContext;
    private readonly MealPlannerDbContextSqLite sqliteDbContext;

    public MigrationController(MealPlannerDbContext sqlDbContext, MealPlannerDbContextSqLite sqliteDbContext)
    {
      this.sqlDbContext = sqlDbContext;
      this.sqliteDbContext = sqliteDbContext;
    }

    [HttpPost]
    public ActionResult Migrate()
    {
      var old = sqlDbContext.Recipes;
      foreach (var recipe in old)
      {
        sqliteDbContext.Recipes.Add(recipe);
      }
      sqliteDbContext.SaveChanges();

      return Ok();
    }
  }
}