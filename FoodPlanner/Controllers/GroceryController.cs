using FoodPlanner.Data.Interfaces;
using FoodPlanner.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodPlanner.Controllers
{
    [Authorize]
    public class GroceryController : Controller
    {
        private readonly IGroceryRepository _groceryRepository;
        public GroceryController(IGroceryRepository groceryRepository) 
        {
            _groceryRepository = groceryRepository;
        }

        public IActionResult Index() => View();

        [HttpPost]
        public IActionResult Index(Grocery grocery)
        {
            if (ModelState.IsValid)
            {
                _groceryRepository.AddGrocery(grocery);
                return RedirectToAction("GrocerySaved");
            }
            else
            {
                return View(grocery);
            }
        }

        public  IActionResult GrocerySaved()
        {
            return View();
        }
    }
}