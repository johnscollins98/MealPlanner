using MealPlanner;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

builder.Services.AddDbContextPool<MealPlannerDbContextSqLite>(options =>
{
  var path = Path.Join(Directory.GetCurrentDirectory(), "App_Data");
  Directory.CreateDirectory(path);
  options.UseSqlite($"Data Source={Path.Join(path, "meals-db.db")}");
});

builder.Services.AddAuthentication(o =>
{
  o.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
  o.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
})
.AddCookie()
.AddGoogle(o =>
{
  o.ClientId = builder.Configuration["Authentication:Google:ClientId"];
  o.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
});

builder.Services.AddAutoMapper(autoMapperBuilder =>
{
  autoMapperBuilder.CreateMap<RecipeEntity, RecipeCreationDto>();
  autoMapperBuilder.CreateMap<RecipeCreationDto, RecipeEntity>();
  autoMapperBuilder.CreateMap<RecipeEntity, RecipeListEntryDto>();
  autoMapperBuilder.CreateMap<RecipeEntity, RecipeDetailsDto>();
});

builder.Services.AddScoped<IRecipeRepository, SqlRecipeRepository>();
builder.Services.AddSingleton<IMealPlanGenerator, BasicRandomMealPlanGenerator>();
builder.Services.AddTransient<IMealPlanRepository, MealPlanRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
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

app.Run();