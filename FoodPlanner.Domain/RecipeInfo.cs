using System.Collections.Generic;

namespace FoodPlanner.Domain
{
    public class RecipeInfo
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ReferenceInfo { get; set; }
        public string ImageUrl { get; set; }
        public string ImageThumbnailUrl { get; set; }
        public List<RecipeInfoRecipe> RecipeInfoRecipes { get; set; }
    }
}
