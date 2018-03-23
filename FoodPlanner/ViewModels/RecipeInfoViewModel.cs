using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodPlanner.Domain;

namespace FoodPlanner.ViewModels
{
    public class RecipeInfoViewModel
    {
        public RecipeInfo RecipeInfo { get; set; }
        public List<Recipe> RecipeList { get; set; }
    }
}
