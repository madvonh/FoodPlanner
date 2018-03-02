using FoodPlanner.Domain;

namespace FoodPlanner.Data.Interfaces
{
    public interface IGroceryRepository
    {
        void AddGrocery(Grocery grocery);
    }
}
