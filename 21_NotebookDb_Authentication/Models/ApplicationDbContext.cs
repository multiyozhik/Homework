using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Identity;

namespace _21_NotebookDb.Models
{
    public class ApplicationDbContext: IdentityDbContext<ApplicationUser>
    {
       
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.EnsureCreated();
            
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //builder.Entity<IdentityRole>().HasData(new IdentityRole() { Name = "admin" });
            //var admin = new ApplicationUser() { UserName = "admin" };
            //admin.PasswordHash = new PasswordHasher<ApplicationUser>()
            //            .HashPassword(admin, "admin123");
            //builder.Entity<ApplicationUser>().HasData(admin);




        }
    }
}
