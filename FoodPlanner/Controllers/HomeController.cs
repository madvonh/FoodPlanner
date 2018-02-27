using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodPlanner.Data.Interfaces;
using FoodPlanner.Domain;
using FoodPlanner.ViewModels;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FoodPlanner.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRecipeInfoRepository _recipeInfoRepository;        // GET: /<controller>/

        public HomeController(IRecipeInfoRepository recipeInfoRepository)
        {
            _recipeInfoRepository = recipeInfoRepository;
        }

        public IActionResult Index()
        {

            var recipeInfoList = _recipeInfoRepository.GetAllRecipeInfo().OrderBy(prop => prop.Title);

            var homeViewModel = new HomeViewModel()
            {
                Title = "Recipes overview",
                RecipInfoList = recipeInfoList.ToList()
            };

            return View(homeViewModel);
        }
        public IActionResult Details(int id)
        {
            var recipeInfo = _recipeInfoRepository.GetRecipeInfoById(id);
            if (recipeInfo == null)
                return NotFound();

            return View(recipeInfo);
        }
    }
}
