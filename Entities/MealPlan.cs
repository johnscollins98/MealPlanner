using System.Collections.Generic;

namespace MealPlanner
{
    public class MealPlan
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public ICollection<Recipe> Recipes { get; set; }
    }
}