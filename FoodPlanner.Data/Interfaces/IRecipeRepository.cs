using FoodPlanner.Domain;
using System.Collections.Generic;

namespace FoodPlanner.Data.Interfaces
{
    public interface IRecipeRepository
    {
        IEnumerable<Recipe> GetAllRecipe();

        Recipe GetRecipeById(int recipeId);
    }
}
