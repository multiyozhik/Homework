using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using WpfClientApp.Models;

namespace WpfClientApp.ViewModels
{
    public class ContactsDbContext: DbContext
    {
        private static DbContextOptions<ContactsDbContext> options = ConfigurateApp(
            "AppConfig.json", "ContactsConString");

        public DbSet<Contact> Contacts { get; set; } 
        public ContactsDbContext() : base(options)
        {
            Database.EnsureCreated(); 		
        }

        //метод возвращает параметры конфигурации на основе ключа - строка подключения к БД
        private static DbContextOptions<ContactsDbContext> ConfigurateApp(
            string configFileName, 
            string keyName)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(configFileName)
                .Build();
            var connectionString = config.GetConnectionString(keyName);
            return new DbContextOptionsBuilder<ContactsDbContext>()                
                .UseSqlServer(connectionString)
                .Options;
        }
    }
}
