using _21_NotebookDb.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;

//"AppConString": "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SarathlalDB;Integrated Security=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"


var builder = WebApplication.CreateBuilder(args);

var appConString = builder.Configuration.GetConnectionString("AppConString");
var contactsConString = builder.Configuration.GetConnectionString("ContactsConString");

builder.Services
    .AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(appConString))
    .AddDbContext<ContactsDbContext>(options => options.UseSqlServer(contactsConString));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
               .AddEntityFrameworkStores<ApplicationDbContext>()
               .AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
     {
         options.SaveToken = true;
         options.RequireHttpsMetadata = false;
         options.TokenValidationParameters = new TokenValidationParameters()
         {
             ValidateIssuer = true,
             ValidateAudience = true,
             ValidAudience = builder.Configuration["JWT:ValidAudience"],
             ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
             IssuerSigningKey = new SymmetricSecurityKey(
                 Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
         };
     });
builder.Services.AddAuthorization();
builder.Services.AddTransient<HomeModel>().AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();


//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapControllerRoute(
//        name: "default",
//        pattern: "{controller=Home}/{action=Index}");
//    endpoints.MapRazorPages();
//});

//ѕри вызове UseAuthentication и UseAuthorization добавл€етс€ ѕќ промежуточного сло€
//дл€ проверки подлинности и авторизации. Ёто ѕќ промежуточного сло€ размещаетс€ между
//методами UseRouting и UseEndpoints, чтобы оно могло:
//просматривать, кака€ конечна€ точка выбрана методом UseRouting;
//примен€ть политику авторизации до отправки UseEndpoints на конечную точку.


app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
//app.MapControllerRoute(name: "default", pattern: "{controller=register}");
//app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}");

app.UseStaticFiles(); //чтобы видел wwwroot папку

await app.RunAsync();

