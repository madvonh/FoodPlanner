using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodPlanner.Domain
{
    public class RecipeInfoRecipe
    {
        public int RecipeInfoId { get; set; }
        public RecipeInfo RecipeInfo { get; set; }
        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }

  
    }
}
