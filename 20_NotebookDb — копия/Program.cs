using _21_AuthNotebookDb;
using _21_AuthNotebookDb.Autherization;
using _21_AuthNotebookDb.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

var appConString = builder.Configuration.GetConnectionString("AppConString");
var contactsConString = builder.Configuration.GetConnectionString("ContactsConString");

builder.Services
    .AddDbContext<AppUsersDbContext>(options => options.UseSqlServer(appConString))
    .AddDbContext<ContactsDbContext>(options => options.UseSqlServer(contactsConString));

builder.Services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<AppUsersDbContext>()
                .AddDefaultTokenProviders();

//builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
//    .AddCookie(options =>
//    {
//        //options.Cookie.HttpOnly = true;
//        options.LoginPath = "/Auth/Login";
//        //options.LogoutPath = "/Auth/Logout";
//        //options.SlidingExpiration = true;
//    });
builder.Services.ConfigureApplicationCookie(o => o.LoginPath = "/Auth/Login");

//builder.Services.AddAuthorization();
builder.Services.AddControllersWithViews();



builder.Services.Configure<IdentityOptions>(options =>
{
    //options.Password.RequiredLength = 6; // минимальное количество знаков в пароле
    //options.Lockout.MaxFailedAccessAttempts = 10; // количество попыток о блокировки
    //options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
    //options.Cookie.Expiration = TimeSpan.FromMinutes(30);
    options.Lockout.AllowedForNewUsers = true;
});

builder.Services
    .AddTransient<HomeModel>();

var app = builder.Build();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

//app.UseEndpoints(config => 
//{
//    config.MapDefaultControllerRoute();
//});

app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseStaticFiles(); //чтобы видел wwwroot папку

await app.RunAsync();



