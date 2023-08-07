using _17_EFShop.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace _17_EFShop.ViewModels
{
	//вначале вызывается статический конструктор и инициализируется закрытое статическое поле options, а
	//затем в конструкторе идет обращение в конструктору базового класса DbContext с параметром options
	class ApplicationDbContext : DbContext
	{
		private static readonly StreamWriter logStream = new StreamWriter("log.txt", true)
		{ AutoFlush = true };
		private static DbContextOptions<ApplicationDbContext> options = ConfigurateApp(
			"AppJsConfig.json", "DefaultConnection", logStream);

		public DbSet<Client> Clients { get; set; } //!если указать не с-ва, а поля, работать не будет!
		public DbSet<Shopping> Shoppings { get; set; }

		public ApplicationDbContext() : base(options)
		{
			Database.EnsureCreated(); //если БД не создана - создается БД на основе Models		
		}

		//метод возвращает параметры конфигурации на основе ключа из json-файла (значение ключа - строка подключения к БД)
		private static DbContextOptions<ApplicationDbContext> ConfigurateApp(string configFileName, string keyName, StreamWriter logStreamWriter)
		{
			var config = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile(configFileName)
				.Build();
			var connectionString = config.GetConnectionString(keyName);
			return new DbContextOptionsBuilder<ApplicationDbContext>()
				.LogTo(logStreamWriter.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information)
				.UseSqlServer(connectionString)
				.Options;
		}

		//для закрытия потока logStreamWriter переопределяем Dispose()
		public override void Dispose()
		{			
			logStream.Dispose();
			base.Dispose();
		}
	}
}
