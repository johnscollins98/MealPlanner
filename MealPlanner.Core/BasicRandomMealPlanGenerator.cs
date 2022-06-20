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

            var chosenBreakfasts = breakfasts.Take(2);
            var chosenLunches = new List<Recipe>
                {
                    lunches.FirstOrDefault(r => r.Category == MealCategory.Meat || r.Category == MealCategory.Poultry),
                    lunches.FirstOrDefault(r => r.Category == MealCategory.Vegetarian)
                }.Where(r => r != null);
            var chosenDinners = new List<Recipe>
                {
                    dinners.FirstOrDefault(r => r.Category == MealCategory.Meat),
                    dinners.FirstOrDefault(r => r.Category == MealCategory.Poultry),
                    dinners.FirstOrDefault(r => r.Category == MealCategory.Vegetarian),
                }.Where(r => r != null);
            var chosenSnacks = new List<Recipe>
                {
                    snacks.FirstOrDefault(r => r.Category == MealCategory.Sweet),
                    snacks.FirstOrDefault(r => r.Category == MealCategory.Savoury),
                }.Where(r => r != null);

            var plan = new MealPlan
            {
                Recipes = chosenBreakfasts.Concat(chosenLunches).Concat(chosenDinners).Concat(chosenSnacks).ToList()
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