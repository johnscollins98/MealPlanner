using System;
using System.Collections.Generic;

namespace MealPlanner.Core
{
    public class MealPlan
    {
        public int Id { get; set; }
        public ICollection<Recipe> Recipes { get; set; }
    }
}