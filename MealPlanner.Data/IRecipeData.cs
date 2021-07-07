using MealPlanner.Core;
using System;
using System.Collections.Generic;

namespace MealPlanner.Data
{
    public interface IRecipeData
    {
        IEnumerable<Recipe> GetRecipes(
            string name = null,
            MealCategory? category = null,
            MealTime? mealTime = null
            );
        Recipe GetRecipe(int id);
        Recipe Add(Recipe recipe);
        Recipe Delete(int id);
        Recipe Update(Recipe recipe);
        int Commit();
    }
}
