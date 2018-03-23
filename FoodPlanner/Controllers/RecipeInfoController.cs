using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using FoodPlanner.Data.Interfaces;
using FoodPlanner.Domain;
using FoodPlanner.Storage.Interfaces;
using FoodPlanner.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodPlanner.Controllers
{
    public class RecipeInfoController : Controller
    {
        private readonly IRecipeInfoRepository _recipeInfoRepository;
        private readonly IRecipeRepository _recipeRepository;

        public RecipeInfoController(IRecipeInfoRepository recipeInfoRepository, IRecipeRepository recipeRepository)
        {
            _recipeInfoRepository = recipeInfoRepository;
            _recipeRepository = recipeRepository;
        }

        public IActionResult Index() => View();


        [HttpPost]
        public IActionResult Index(RecipeInfoViewModel recipeInfoViewModel)
        {
            if (ModelState.IsValid)
            {
                var recipeInfo = new RecipeInfo()
                {
                    Title =  recipeInfoViewModel.RecipeInfo.Title,
                    Description = recipeInfoViewModel.RecipeInfo.Description,
                    ImageUrl = recipeInfoViewModel.RecipeInfo.ImageUrl
                };
                _recipeInfoRepository.AddRecipeInfo(recipeInfo);

                if(recipeInfoViewModel.RecipeList==null || recipeInfoViewModel.RecipeList.Count<1) return RedirectToAction("RecipeInfoSaved");

                var recipeList = new List<Recipe>();
                foreach (var r in recipeInfoViewModel.RecipeList)
                {

                    var recipe = new Recipe()
                    { 
                        Name = r.Name,
                        LongDescription = r.LongDescription,
                        ShortDescription = r.ShortDescription,
                        PiecesOfJobs = r.PiecesOfJobs,

                    };
                    recipeList.Add(recipe);
                }

                _recipeRepository.AddRecipeList(recipeList);

                return RedirectToAction("RecipeInfoSaved");
            }
            else
            {
                return View(recipeInfoViewModel);
            }
        }

        public IActionResult RecipeInfoSaved()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> Upload(IFormFile image, IAzureBlobStorage storage)
        {
            if (image != null)
            {
                string id = Guid.NewGuid().ToString();

                await storage.UploadAsync(id, image.FileName);

                return RedirectToAction("Show", new { id = id });
            }
            return PartialView("_Image");
        }

        public async Task<ActionResult>  Show(string id, IAzureBlobStorage storage)
        {
            var model = new ShowModel { Uri = await storage.UriFor(id) };
            //return View(model);
            return PartialView("_Image");
        }


    }
}