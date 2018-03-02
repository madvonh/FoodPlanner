using FoodPlanner.Data.Interfaces;
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
