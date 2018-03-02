using FoodPlanner.Domain;
using System.Collections.Generic;

namespace FoodPlanner.ViewModels
{
    public class HomeViewModel
    {
        public string Title { get; set; }

        public List<RecipeInfo> RecipInfoList { get; set; }
    }
}
