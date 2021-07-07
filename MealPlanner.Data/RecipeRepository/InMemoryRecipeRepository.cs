using MealPlanner.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace MealPlanner.Data
{
    public class InMemoryRecipeRepository : IRecipeRepository
    {
        public List<Recipe> Recipes { get; set; }

        public InMemoryRecipeRepository()
        {
            Recipes = new List<Recipe>
            {
                new Recipe
                {
                    ID = 1,
                    Name = "Soup",
                    Category = MealCategory.Vegetarian,
                    Time = MealTime.Lunch
                },
                new Recipe
                {
                    ID = 2,
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
                    ID = 4,
                    Name = "Chicken Wraps",
                    Category = MealCategory.Poultry,
                    Time = MealTime.Dinner
                },
                new Recipe
                {
                    ID = 5,
                    Name = "Stuffed Peppers",
                    Category = MealCategory.Vegetarian,
                    Time = MealTime.Dinner
                },

                new Recipe
                {
                    ID = 6,
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

        public Recipe Get(int id)
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

        public IEnumerable<Recipe> Find(Expression<Func<Recipe, bool>> predicate)
        {
            return Recipes
                .AsQueryable()
                .Where(predicate)
                .ToList();
        }

        public IEnumerable<Recipe> All()
        {
            return Recipes;
        }
    }
}
