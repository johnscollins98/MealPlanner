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
            var breakfasts = GetByMealTime(MealTime.Breakfast, recipes);
            var lunches = GetByMealTime(MealTime.Lunch, recipes);
            var dinners = GetByMealTime(MealTime.Dinner, recipes);
            var snacks = GetByMealTime(MealTime.Snack, recipes);

            var plan = new MealPlan
            {
                Breakfasts = breakfasts.Take(2),

                Lunches = new List<Recipe>
                {
                    lunches.FirstOrDefault(r => r.Category == MealCategory.Meat),
                    lunches.FirstOrDefault(r => r.Category == MealCategory.Vegetarian)
                }.Where(r => r != null),

                Dinners = new List<Recipe>
                {
                    dinners.FirstOrDefault(r => r.Category == MealCategory.Meat),
                    dinners.FirstOrDefault(r => r.Category == MealCategory.Poultry),
                    dinners.FirstOrDefault(r => r.Category == MealCategory.Vegetarian),
                }.Where(r => r != null),

                Snacks = new List<Recipe>
                {
                    snacks.FirstOrDefault(r => r.Category == MealCategory.Sweet),
                    snacks.FirstOrDefault(r => r.Category == MealCategory.Savoury),
                }.Where(r => r != null)
            };

            return plan;
        }

        private IEnumerable<Recipe> GetByMealTime(MealTime time, IEnumerable<Recipe> recipes)
        {
            var query = recipes
                .Where(r => r.Time == time)
                .OrderBy(r => Guid.NewGuid());

            return query;
        }
    }
}