using EsportsPredictor.Interfaces;
using EsportsPredictor.Services;
using Microsoft.EntityFrameworkCore;
using EsportsPredictor.DataAccess;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IPandascoreApiService, PandascoreApiService>();
builder.Services.AddDbContext<EsportsPredictorContext>(
    options => options.UseNpgsql(
        builder.Configuration["DbConnectionString"]
        ?? throw new InvalidOperationException("Database connection string not found")
        )
    .UseSnakeCaseNamingConvention()
);

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
