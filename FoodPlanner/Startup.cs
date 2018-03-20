using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using FoodPlanner.Data.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using FoodPlanner.Data;
using FoodPlanner.Data.Repositories;
using Microsoft.AspNetCore.Identity;
using FoodPlanner.Services;
using FoodPlanner.Storage;
using FoodPlanner.Storage.Interfaces;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace FoodPlanner
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //register our own services
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IAzureBlobStorage>(factory => new AzureBlobStorage(new AzureBlobSettings(
                storageAccount: Configuration["StorageAccount"],
                storageKey: Configuration["StorageKey"],
                containerName: Configuration["ContainerName"])));

            //Configuration.GetConnectionString("StorageConnection"),

            services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();

            services.AddTransient<IRecipeInfoRepository, RecipeInfoRepository>();
            services.AddTransient<IGroceryRepository, GroceryRepository>();

            services.AddSingleton<IGreeter, Greeter>();

            //register framework services
  
            services.AddMvc();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
            //IConfiguration config
            AppDbContext context,
            IGreeter greeter
            )
        {
            context.Database.Migrate();
            //if( env.IsDevelopment())
            //{ 
                 app.UseDeveloperExceptionPage();
            //}
            //app.Run(async (context) =>
            //{
            //    var greeting = Configuration["Greeting"];
            //    await context.Response.WriteAsync(greeting);

            //});
            //app.Run(async (context) =>
            //{
            //    var greeting = greeter.GetMessageOfTheDay(); 
            //    await context.Response.WriteAsync(greeting);

            //});
            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}"
                    );
            }
            );
        }
    }
}
