using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using _21_AuthNotebookDb.Models;
using System.Reflection.Emit;

namespace _21_AuthNotebookDb.Autherization
{
    public class AppUsersDbContext: IdentityDbContext<AppUser>
    {
        
        public AppUsersDbContext(DbContextOptions<AppUsersDbContext> options) : base(options)
        {
            //Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
        }
    }
}
