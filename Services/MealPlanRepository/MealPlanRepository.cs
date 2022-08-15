using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Security.Claims;

namespace MealPlanner;
public class MealPlanRepository : IMealPlanRepository
{
  private readonly MealPlannerDbContextSqLite db;
  public MealPlanRepository(MealPlannerDbContextSqLite db)
  {
    this.db = db ?? throw new ArgumentNullException(nameof(db));
  }

  public MealPlanEntity Add(MealPlanEntity entity)
  {
    db.Add(entity);
    return entity;
  }

  public IEnumerable<MealPlanEntity> All()
  {
    return db.MealPlans.Include(plan => plan.Recipes);
  }

  public int Commit()
  {
    return db.SaveChanges();
  }

  public MealPlanEntity? Delete(int id)
  {
    var mealPlan = Get(id);
    if (mealPlan != null)
    {
      db.MealPlans.Remove(mealPlan);
    }
    return mealPlan;
  }

  public IEnumerable<MealPlanEntity> Find(Expression<Func<MealPlanEntity, bool>> predicate)
  {
    var query = db.MealPlans
        .Where(predicate)
        .Include(plan => plan.Recipes);

    return query;
  }

  public MealPlanEntity? Get(int id)
  {
    return db.MealPlans.Find(id);
  }

  public MealPlanEntity? GetMealPlanForUser(ClaimsPrincipal user)
  {
    return Find(mp => mp.UserId == user.GetNameIdentifier()).FirstOrDefault();
  }

  public MealPlanEntity Update(MealPlanEntity entity)
  {
    var existing = db.MealPlans.Attach(entity);
    existing.State = EntityState.Modified;
    return entity;
  }
}
