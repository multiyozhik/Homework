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
		private static readonly StreamWriter logStream = new StreamWriter("log.txt", true);
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
			var configurationBuilder = new ConfigurationBuilder();
			configurationBuilder.SetBasePath(Directory.GetCurrentDirectory());
			configurationBuilder.AddJsonFile(configFileName);
			var config = configurationBuilder.Build();
			var connectionString = config.GetConnectionString(keyName);
			var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();			
			var appOptions = optionsBuilder.UseSqlServer(connectionString).Options;
			//настройки логгирования (только информационного характера) -
			//запись в поток logStream (в txt-файл, Append=true)
			optionsBuilder.LogTo(logStreamWriter.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);
			return appOptions;
		}

		//для закрытия потока logStreamWriter переопределяем Dispose()
		public override void Dispose()
		{
			base.Dispose();
			logStream.Dispose();
		}
	}
}
