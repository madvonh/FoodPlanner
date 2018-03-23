using FoodPlanner.Data.Interfaces;
using FoodPlanner.Domain;
using System.Collections.Generic;
using System.Linq;

namespace FoodPlanner.Data.Repositories
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly AppDbContext _appDbContext;
        public RecipeRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<Recipe> GetAllRecipe()
        {
            return _appDbContext.Recipes;
        }

        public Recipe GetRecipeById(int recipeId)
        {
            return _appDbContext.Recipes.FirstOrDefault(r => r.Id == recipeId);
        }

        public void AddRecipe(Recipe recipe)
        {
            _appDbContext.Recipes.Add(recipe);
            _appDbContext.SaveChanges();
        }

        public void AddRecipeList(List<Recipe> recipelist)
        {
            _appDbContext.Recipes.AddRange(recipelist);
            _appDbContext.SaveChanges();
        }
    }
}
