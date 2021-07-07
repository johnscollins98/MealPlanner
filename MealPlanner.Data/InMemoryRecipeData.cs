using MealPlanner.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MealPlanner.Data
{
    public class InMemoryRecipeData : IRecipeData
    {
        public List<Recipe> Recipes { get; set; }

        public InMemoryRecipeData()
        {
            Recipes = new List<Recipe>
            {
                new Recipe
                {
                    ID = 0,
                    Name = "Soup",
                    Category = MealCategory.Vegetarian,
                    Time = MealTime.Lunch
                },
                new Recipe
                {
                    ID = 1,
                    Name = "Burger",
                    Category = MealCategory.Meat,
                    Time = MealTime.Dinner
                },
                new Recipe
                {
                    ID = 3,
                    Name = "Hot Dog",
                    Category = MealCategory.Meat,
                    Time = MealTime.Dinner
                },
                new Recipe
                {
                    ID = 5,
                    Name = "Chicken Wraps",
                    Category = MealCategory.Poultry,
                    Time = MealTime.Dinner
                },
                new Recipe
                {
                    ID = 7,
                    Name = "Stuffed Peppers",
                    Category = MealCategory.Vegetarian,
                    Time = MealTime.Dinner
                },

                new Recipe
                {
                    ID = 2,
                    Name = "Cereal",
                    Category = MealCategory.Vegetarian,
                    Time = MealTime.Breakfast
                },
            };
        }

        public Recipe Add(Recipe recipe)
        {
            recipe.ID = Recipes.Count() > 0 ? Recipes.Max(r => r.ID) + 1 : 1;
            Recipes.Add(recipe);
            return recipe;
        }

        public int Commit()
        {
            return 0;
        }

        public Recipe Delete(int id)
        {
            var recipe = Recipes.FirstOrDefault(r => r.ID == id);
            if (recipe != null)
            {
                Recipes.Remove(recipe);
            }
            return recipe;
        }

        public IEnumerable<Recipe> GetRecipes(string name, MealCategory? category, MealTime? mealTime)
        {
            return from r in Recipes
                   orderby r.Time, r.Category, r.Name
                   where string.IsNullOrWhiteSpace(name) || r.Name.Contains(name, StringComparison.CurrentCultureIgnoreCase)
                   where category == null || r.Category == category
                   where mealTime == null || r.Time == mealTime
                   select r;
        }

        public Recipe GetRecipe(int id)
        {
            return Recipes.FirstOrDefault(r => r.ID == id);
        }

        public Recipe Update(Recipe newRecipe)
        {
            var recipe = Recipes.SingleOrDefault(r => r.ID == newRecipe.ID);
            if (recipe != null)
            {
                recipe.Name = newRecipe.Name;
                recipe.Time = newRecipe.Time;
                recipe.Category = newRecipe.Category;
            }
            return recipe;
        }
    }
}
