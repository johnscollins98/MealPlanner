using System;
using System.IO;
using MealPlanner.Core;
using MealPlanner.Data;
using MealPlanner.Data.MealPlanRepository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MealPlanner
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();

            services.AddDbContextPool<MealPlannerDbContextSqLite>(options => 
            {
                var path = Path.Join(Directory.GetCurrentDirectory(), "App_Data");
                Directory.CreateDirectory(path);
                options.UseSqlite($"Data Source={Path.Join(path, "meals-db.db")}");
            });

            services.AddAuthentication(o => 
            {
                o.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
            })
            .AddCookie()
            .AddGoogle(o => 
            {
                o.ClientId = Configuration["Authentication:Google:ClientId"];
                o.ClientSecret = Configuration["Authentication:Google:ClientSecret"];
            });

            services.AddAutoMapper(autoMapperBuilder => 
            {
                autoMapperBuilder.CreateMap<Recipe, RecipeCreationDto>();
                autoMapperBuilder.CreateMap<RecipeCreationDto, Recipe>();
                autoMapperBuilder.CreateMap<Recipe, RecipeListEntryDto>();
                autoMapperBuilder.CreateMap<Recipe, RecipeDetailsDto>();
            });

            services.AddScoped<IRecipeRepository, SqlRecipeRepository>();
            services.AddSingleton<IMealPlanGenerator, BasicRandomMealPlanGenerator>();
            services.AddTransient<IMealPlanRepository, MealPlanRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
