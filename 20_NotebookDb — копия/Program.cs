using _21_AuthNotebookDb.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services
    .AddDbContext<ContactsDbContext>(options => options.UseSqlServer(connectionString))
    .AddTransient<HomeModel>()
    .AddControllersWithViews();

var app = builder.Build();

app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseStaticFiles(); //чтобы видел wwwroot папку

await app.RunAsync();
