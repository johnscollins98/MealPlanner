using MealPlanner.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace MealPlanner.Data.MealPlanRepository
{
    public class MealPlanRepository : IMealPlanRepository
    {
        private readonly MealPlannerDbContextSqLite db;
        public MealPlanRepository(MealPlannerDbContextSqLite db)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public MealPlan Add(MealPlan entity)
        {
            db.Add(entity);
            return entity;
        }

        public IEnumerable<MealPlan> All()
        {
            return db.MealPlans.Include(plan => plan.Recipes);
        }

        public int Commit()
        {
            return db.SaveChanges();
        }

        public MealPlan Delete(int id)
        {
            var mealPlan = Get(id);
            if (mealPlan != null)
            {
                db.MealPlans.Remove(mealPlan);
            }
            return mealPlan;
        }

        public IEnumerable<MealPlan> Find(Expression<Func<MealPlan, bool>> predicate)
        {
            var query = db.MealPlans
                .Where(predicate)
                .Include(plan => plan.Recipes);

            return query;
        }

        public MealPlan Get(int id)
        {
            return db.MealPlans.Find(id);
        }

        public MealPlan Update(MealPlan entity)
        {
            var existing = db.MealPlans.Attach(entity);
            existing.State = EntityState.Modified;
            return entity;
        }
    }
}
