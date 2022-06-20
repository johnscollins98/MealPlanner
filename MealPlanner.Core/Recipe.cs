using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MealPlanner.Core
{
    public class Recipe
    {
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public MealCategory Category { get; set; }

        [Required]
        public MealTime Time { get; set; }

        [Required]
        public int Calories { get; set; }

        public string Notes { get; set; }

        public ICollection<MealPlan> MealPlans { get; set; }

    }
}
