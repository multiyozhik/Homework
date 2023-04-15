using System;
using System.IO;

namespace EmployeesDirectory
{
	class Program
	{
		static void Main()
		{
			Console.WriteLine("Выберите вариант вывода данных:" +
							  "\n1 - вывести данные на экран;" +
							  "\n2 - заполнить данные и добавить новую запись в конец файла");
			var choose = Console.ReadLine();
			var fileName = "EmployeesDB.txt";							// задаем название файла, куда будет идти запись сотрудников
			var path = Path.Combine(Directory.GetCurrentDirectory(), fileName); // формирование пути в текущей bin... папке
			switch (choose)                    // при запуске программы выбор вывода данных
			{
				case "1":
				{
					if (File.Exists(path))
					{
						WritingConsole(path);	// метод вывода на консоль
					}
					break;
				}
				case "2":
				{
					string record = InputData();	// ввод данных с консоли
					WritingToEndFile(path, record);	// запись в конец файла
					break;
				}
				default:
					Console.WriteLine("Ошибка ввода: необходимо ввести 1 или 2");
					break;
			}
		}
		public static string InputData()						   // ввод данных с консоли
		{
			Console.WriteLine("Введите данные по сотруднику");
			Console.WriteLine("Id");
			var id = Console.ReadLine();
			var date = $"{DateTime.Now:dd.MM.yyyy hh:mm}";
			Console.WriteLine("Ф.И.О.");
			var fullName= Console.ReadLine();
			Console.WriteLine("Возраст");
			var age = Console.ReadLine();
			Console.WriteLine("Рост");
			var height = Console.ReadLine();
			Console.WriteLine("Дата рождения");
			var bithday = Console.ReadLine();
			Console.WriteLine("Место рождения");
			var bithPlace = Console.ReadLine();
			return String.Join('#', id, date, fullName, age, height, bithday, bithPlace)+"\r\n"; 
		}
		public static void WritingConsole(string path)          // метод вывода на консоль
		{
			var records = File.ReadAllLines(path);      // ReadAllLines считывание построчно в массив записей
			foreach (var record in records)				// по каждой записи 
			{
				var substrings = record.Split('#', 7);  // Split разбивает на подстроки (массив подстрок)
				Console.WriteLine($"Id сотрудника {substrings[0]} " +		// и вывод на консоль
				                  $"\nдата записи {substrings[1]} " +
				                  $"\nФ.И.О {substrings[2]}" +
				                  $"\nвозраст {substrings[3]}" +
				                  $"\nрост {substrings[4]}" +
				                  $"\nдень рождения {substrings[5]}" +
				                  $"\nместо рождения {substrings[6]}");
				Console.WriteLine();
			}
			Console.ReadKey();
		}
		public static void WritingToEndFile(string path, string record)     // запись в конец файла
		{
			File.AppendAllText(path,record);        // если файл не создан, AppendAllText создает и добавляет запись
		}
	}
}
