using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FoodPlanner.Domain
{
    public class Grocery
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Manufacturer { get; set; }

        [Required]
        public int CategoryGroceryId { get; set; }
        public CategoryGrocery CategoryGrocery { get; set;}
    
    }
}
