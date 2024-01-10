using _21_NotebookDb.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace _21_NotebookDb
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            //System.InvalidOperationException: "Endpoint Routing does not support
            //'IApplicationBuilder.UseMvc(...)'. To use 'IApplicationBuilder.UseMvc'
            //set 'MvcOptions.EnableEndpointRouting = false' inside 'ConfigureServices(...)."

            services.AddDbContext<ContactsDbContext>(options => options.UseSqlServer(
                Configuration.GetConnectionString("ContactsConString")));
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
                Configuration.GetConnectionString("AppConString")));
            services.AddTransient<HomeModel>();

            services.AddMvc(options => options.EnableEndpointRouting = false);

            #region //

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 6; // минимальное количество знаков в пароле
                options.Lockout.MaxFailedAccessAttempts = 10; // количество попыток о блокировки
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
                options.Lockout.AllowedForNewUsers = true;
            });

            services.ConfigureApplicationCookie(options =>
            {
                // конфигурация Cookie с целью использования их для хранения авторизации
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                //options.Cookie.Expiration = TimeSpan.FromMinutes(30);
                options.LoginPath = "/Authenticate/Login";
                options.LogoutPath = "/Authenticate/Logout";
                options.SlidingExpiration = true;
            });

            #endregion
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
                    //template: "{action=Index}/{id?}");
        });
        }
    }
}
