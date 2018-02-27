using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodPlanner.Domain
{
    public class PieceOfJob
    {
        public int Id { get; set; }
        public int OrderNumber { get; set; }
        public string Description { get; set; }
        public int RecipeId { get; set; }
    }
}
