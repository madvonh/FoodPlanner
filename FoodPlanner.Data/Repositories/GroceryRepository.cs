using System;
using FoodPlanner.Data.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodPlanner.Domain;

    namespace FoodPlanner.Data.Repositories
{
    public class GroceryRepository : IGroceryRepository
    {
        private readonly AppDbContext _appDbContext;
        public GroceryRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;

        }
        public void AddGrocery(Grocery grocery)
        {
            _appDbContext.Groceries.Add(grocery);
            _appDbContext.SaveChanges();
        }
    }
}
