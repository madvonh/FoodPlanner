﻿using FoodPlanner.Data.Interfaces;
using FoodPlanner.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
    }
}