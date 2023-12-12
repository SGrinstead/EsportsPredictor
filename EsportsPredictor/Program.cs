using EsportsPredictor.Interfaces;
using EsportsPredictor.Services;
using Microsoft.EntityFrameworkCore;
using EsportsPredictor.DataAccess;
using Microsoft.AspNetCore.Identity;
using EsportsPredictor.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<EsportsPredictorContext>(options =>
	options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<EsportsPredictorContext>();
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IPandascoreApiService, PandascoreApiService>();
//builder.Services.AddDbContext<EsportsPredictorContext>(
//    options => options.UseNpgsql(
//        builder.Configuration["DbConnectionString"]
//        ?? throw new InvalidOperationException("Database connection string not found")
//        )
//    .UseSnakeCaseNamingConvention()
//);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();;

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
