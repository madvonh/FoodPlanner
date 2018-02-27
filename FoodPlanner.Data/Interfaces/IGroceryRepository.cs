﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodPlanner.Domain;

namespace FoodPlanner.Data.Interfaces
{
    public interface IGroceryRepository
    {
        void AddGrocery(Grocery grocery);
    }
}