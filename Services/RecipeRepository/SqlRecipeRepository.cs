using MealPlanner.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;

namespace MealPlanner.Data
{
    public class SqlRecipeRepository : IRecipeRepository
    {
        private readonly MealPlannerDbContextSqLite db;

        public SqlRecipeRepository(MealPlannerDbContextSqLite db)
        {
            this.db = db;
        }

        public Recipe Add(Recipe entity)
        {
            db.Add(entity);
            return entity;
        }

        public IEnumerable<Recipe> All()
        {
            return db.Recipes.OrderBy(r => r.Time).ThenBy(r => r.Category).ThenBy(r => r.Name);
        }

        public int Commit()
        {
            return db.SaveChanges();
        }

        public Recipe Delete(int id)
        {
            var recipe = Get(id);
            if (recipe != null)
            {
                db.Recipes.Remove(recipe);
            }
            return recipe;
        }

        public IEnumerable<Recipe> Find(Expression<Func<Recipe, bool>> predicate)
        {
            var query = db.Recipes
                .Where(predicate)
                .OrderBy(r => r.Time)
                .ThenBy(r => r.Category)
                .ThenBy(r => r.Name);

            return query;
        }

        public Recipe Get(int id)
        {
            return db.Recipes.Find(id);
        }

        public Recipe Update(Recipe entity)
        {
            var existing = db.Recipes.Attach(entity);
            existing.State = EntityState.Modified;
            return entity;
        }

        public IEnumerable<Recipe> GetRecipesForUser(ClaimsPrincipal user)
        {
            return Find(r => r.UserId == user.GetNameIdentifier());
        }
    }
}
