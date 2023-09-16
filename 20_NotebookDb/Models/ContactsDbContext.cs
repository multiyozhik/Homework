using Microsoft.EntityFrameworkCore;

namespace _20_NotebookDb.Models
{
    public class ContactsDbContext : DbContext
    {
        public DbSet<Contact> Contacts { get; protected set; }
        public ContactsDbContext(DbContextOptions<ContactsDbContext> options) : base(options)
            => Database.EnsureCreated();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contact>().HasData(
                new Contact(Guid.NewGuid(), "Петров", "Петр", "Петрович", "+79504112233", "Москва"),
                new Contact(Guid.NewGuid(), "Иванов", "Иван", "Иванович", "+79524778899", "Липецк"),
                new Contact(Guid.NewGuid(), "Захарова", "Мария", "Кузьминишна", "+79059876543", "Казань"));
        }
    }
}
