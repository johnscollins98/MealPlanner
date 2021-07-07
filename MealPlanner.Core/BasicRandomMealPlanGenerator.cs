using System;
using System.Collections.Generic;
using System.Linq;

namespace MealPlanner.Core
{
    public class BasicRandomMealPlanGenerator : IMealPlanGenerator
    {
        public BasicRandomMealPlanGenerator()
        {
        }

        public MealPlan Generate(IEnumerable<Recipe> recipes)
        {
            return new MealPlan
            {
                Breakfasts = GetByMealTime(MealTime.Breakfast, recipes, 2),
                Lunches = GetByMealTime(MealTime.Lunch, recipes, 2),
                Dinners = GetByMealTime(MealTime.Dinner, recipes, 2),
                Snacks = GetByMealTime(MealTime.Snack, recipes, 1)
            };
        }

        private IEnumerable<Recipe> GetByMealTime(MealTime time, IEnumerable<Recipe> recipes, int num)
        {
            var query = from r in recipes
                        where r.Time == time
                        select r;

            return query.OrderBy(r => Guid.NewGuid()).Take(num);
        }
    }
}