using _21_NotebookDb.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using _21_NotebookDb;
using Microsoft.AspNetCore;

//"AppConString": "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SarathlalDB;Integrated Security=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"



var init = BuildWebHost(args);

using (var scope = init.Services.CreateScope())
{
    var s = scope.ServiceProvider;
    var c = s.GetRequiredService<ContactsDbContext>();
    //DbInitializer.Initialize(c);
}

init.Run();
static IWebHost BuildWebHost(string[] args)
{
    return WebHost.CreateDefaultBuilder(args)
        .UseStartup<Startup>()
        .Build();
}



