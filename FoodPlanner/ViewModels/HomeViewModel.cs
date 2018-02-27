using FoodPlanner.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodPlanner.ViewModels
{
    public class HomeViewModel
    {
        public string Title { get; set; }

        public List<RecipeInfo> RecipInfoList { get; set; }
    }
   
}
