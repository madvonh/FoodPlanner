using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodPlanner.Data.Interfaces;
using FoodPlanner.Domain;


namespace FoodPlanner.Data.Repositories
{
    public class RecipeInfoRepository : IRecipeInfoRepository
    {
        private readonly AppDbContext _appDbContext;
        public RecipeInfoRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<RecipeInfo> GetAllRecipeInfo()
        {
            return _appDbContext.RecipeInfos;
        }

        public RecipeInfo GetRecipeInfoById(int recipeInfoId)
        {
            return _appDbContext.RecipeInfos.FirstOrDefault(r => r.Id == recipeInfoId);
        }
    }
}
