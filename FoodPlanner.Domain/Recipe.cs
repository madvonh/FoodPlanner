using System.Collections.Generic;

namespace FoodPlanner.Domain
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public List<Grocery> Groceries {get; set; }
        public List<PieceOfJob> PiecesOfJobs { get; set; }
        public List<RecipeInfoRecipe> RecipeInfoRecipes { get; set; }
    }
}
