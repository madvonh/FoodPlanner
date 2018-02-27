using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodPlanner.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace FoodPlanner.Data
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        { }
        public DbSet<RecipeInfo> RecipeInfos { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Grocery> Groceries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RecipeInfoRecipe>()
                .HasKey(r => new { r.RecipeId, r.RecipeInfoId });
            base.OnModelCreating(modelBuilder);
        }
    }
}
