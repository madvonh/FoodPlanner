using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using FoodPlanner.Data.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using FoodPlanner.Data;
using FoodPlanner.Data.Repositories;
using FoodPlanner.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Rewrite;

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

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
            .AddOpenIdConnect(options =>
            {
                Configuration.Bind("AzureAd", options);
            })
            .AddCookie();

            services.AddTransient<IRecipeInfoRepository, RecipeInfoRepository>();
            services.AddTransient<IGroceryRepository, GroceryRepository>();

            services.AddSingleton<IGreeter, Greeter>();

            //register framework services
  
            services.AddMvc();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if( env.IsDevelopment())
            { 
                 app.UseDeveloperExceptionPage();
            }
            
            //this ensures all pages will use ssl
            app.UseRewriter(new RewriteOptions().AddRedirectToHttpsPermanent());

            app.UseStatusCodePages();
            app.UseStaticFiles();

            app.UseNodeModules(env.ContentRootPath);

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
