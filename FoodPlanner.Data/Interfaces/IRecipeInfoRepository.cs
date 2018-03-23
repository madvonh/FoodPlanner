using System.Collections.Generic;
using FoodPlanner.Domain;


namespace FoodPlanner.Data.Interfaces
{
    public interface IRecipeInfoRepository
    {
        IEnumerable<RecipeInfo> GetAllRecipeInfo();

        RecipeInfo GetRecipeInfoById(int recipeInfoId);

        void AddRecipeInfo(RecipeInfo recipeinfo);

    }
}
