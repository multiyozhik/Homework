using _19_Notebook.Models;
using _19_Notebook.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddTransient<HomeModel>()
    .AddSingleton<IRepository, Repository>()
    .AddControllersWithViews();

var app = builder.Build();

app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
