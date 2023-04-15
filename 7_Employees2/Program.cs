using System;
using System.IO;


namespace Employees
{
	class Program
	{
		static void Main()
		{
			string fileName = "EmployeeList.txt";		 // исходный файл со списком сотрудников
			string path = Path.Combine(Directory.GetCurrentDirectory(), fileName);
			if (!File.Exists(path))
			{
				Console.WriteLine("просмотр невозможен, файл не существует");
				Console.ReadKey();
				return;
			}

			var employees = ReadingData(path); // метод считывает с файла и создаем массив структур (записей)

			Console.WriteLine("Выберите операцию:" +
							  "\n1 - просмотр записи" +
							  "\n2 - создание записи" +
							  "\n3 - удаление записи" +
							  "\n4 - редактирование записи" +
							  "\n5 - загрузка записей в диапазоне дат" +
							  "\n6 - сортировка записей по возрастанию даты записи" +
							  "\n7 - сортировка записей по убыванию даты записи");

			string operation = Console.ReadLine();
			switch (operation)
			{
				case "1":
					{
						var employeeId = AskForId();
						var index = FindIndexOfEmployee(employees, employeeId);
						if (Check(index))
							employees[index].Show();
						break;
					}
				case "2":
					{
						var newEmployee = Create();
						File.AppendAllText(path, '\n' + newEmployee.ToString());
						break;
					}
				case "3":
					{
						var employeeId = AskForId();
						var index = FindIndexOfEmployee(employees, employeeId);
						if (Check(index))
						{
							var employeesAfterDelete = Delete(employees, index);
							SaveAll(path, employeesAfterDelete);
						}
						break;
					}
				case "4":
					{
						var employeeId = AskForId();
						var index = FindIndexOfEmployee(employees, employeeId);
						if (Check(index))
						{
							var employeesAfterEdit = Edit(employees, index);
							SaveAll(path, employeesAfterEdit);
						}
						break;
					}
				case "5":
					{
						LoadDateRange(employees);
						break;
					}
				case "6":
					{
						Sort(employees);
						SaveAll(path, employees);
						break;
					}
				case "7":
					{
						Sort(employees);
						Array.Reverse(employees);
						SaveAll(path, employees);
						break;
					}
				default:
					{
						Console.WriteLine("некорректный ввод операции");
						break;
					}
			}
			Console.ReadKey();
		}

		/// <summary>
		/// Метод возвращает true, если сотрудник с заданным ID найден, и false, если не найден (с соответствующим сообщением)
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		private static bool Check(int index)
		{
			bool check = false;
			if (index == -1)
				Console.WriteLine("сотрудника с заданным id не существует");
			else
				check = true;
			return check;
		}

		/// <summary>
		/// Метод возвращает индекс массива сотрудников по заданному ID (если Id не найден, то возвращает -1)
		/// </summary>
		/// <param name="employees"></param>
		/// <param name="employeeId"></param>
		/// <returns></returns>
		private static int FindIndexOfEmployee(Employee[] employees, int employeeId)
		{
			for (int i = 0; i < employees.Length; i++)
			{
				if (employees[i].Id == employeeId)
					return i;
			}
			return -1; 
		}

		/// <summary>
		/// Метод возвращает ID сотрудника
		/// </summary>
		/// <returns></returns>
		public static int AskForId()
		{
			Console.WriteLine("Введите номер сотрудника id");
			return int.Parse(Console.ReadLine());
		}

		/// <summary>
		/// Метод для чтения файла списка сотрудников, на входе - путь файла, на выходе - массив структур - записей сотрудников
		/// </summary>
		/// <param name="path"></param>
		/// <returns></returns>
		public static Employee[] ReadingData(string path)
		{
			var lines = File.ReadAllLines(path);
			var employees = new Employee[lines.Length];
			for (int i = 0; i < lines.Length; i++)
			{
				var sublines = lines[i].Split('#');
				employees[i] = new Employee(int.Parse(sublines[0]),
					DateTime.Parse(sublines[1]),
					sublines[2],
					byte.Parse(sublines[3]),
					byte.Parse(sublines[4]),
					DateTime.Parse(sublines[5]),
					sublines[6]);
			}
			return employees;
		}

		/// <summary>
		/// Метод создания записи с данными сотрудника пользователем с консоли
		/// </summary>
		/// <returns></returns>
		public static Employee Create()
		{
			Console.WriteLine($"ID сотрудника");
			var id = int.Parse(Console.ReadLine());
			Console.WriteLine("Дата и время добавления записи");
			var dateRecord = DateTime.Parse(Console.ReadLine());
			Console.WriteLine("Ф.И.О.");
			var fullName = Console.ReadLine();
			Console.WriteLine("Возраст");
			var age = int.Parse(Console.ReadLine());
			Console.WriteLine("Рост");
			var height = int.Parse(Console.ReadLine());
			Console.WriteLine("Дата рождения");
			var bithDay = DateTime.Parse(Console.ReadLine());
			Console.WriteLine("Место рождения");
			var bithPlace = Console.ReadLine();
			return new Employee(id, dateRecord, fullName, age, height, bithDay, bithPlace);
		}

		
		/// <summary>
		/// Метод удаляет запись сотрудника по заданному ID и вывод в файл
		/// </summary>
		/// <param name="path"></param>
		/// <param name="employees"></param>
		/// <param name="id"></param>
		public static Employee[] Delete(Employee[] employees, int indexDeleted)
		{
			var employeesAfterDeleted = new Employee[employees.Length - 1];
			Array.Copy(employees, 0, employeesAfterDeleted, 0, indexDeleted);
			Array.Copy(employees, indexDeleted + 1, employeesAfterDeleted, indexDeleted,
				employees.Length - indexDeleted - 1);
			return employeesAfterDeleted;
		}

		/// <summary>
		/// Метод редактирования записи сотрудника по заданному ID и вывод в файл
		/// </summary>
		/// <param name="path"></param>
		/// <param name="employees"></param>
		/// <param name="index"></param>
		/// <returns></returns>
		public static Employee[] Edit(Employee[] employees, int index)
		{
			Console.WriteLine("Имеющиеся сведения по сотруднику");
			employees[index].Show();
			Console.WriteLine("Введите новые данные по сотруднику");
			employees[index] = Create();
			return employees;
		}

		/// <summary>
		/// Метод вывода на консоль записей сотрудников с датой создания записи в диапазоне дат
		/// </summary>
		/// <param name="pathLoad"></param>
		/// <param name="employees"></param>
		public static void LoadDateRange(Employee[] employees)
		{
			Console.WriteLine("Введите первую дату диапазона дат");
			var firstDate = DateTime.Parse(Console.ReadLine());
			Console.WriteLine("Введите вторую дату диапазона дат");
			var secondDate = DateTime.Parse(Console.ReadLine());
			foreach (var employee in employees)
			{
				bool inRange = (employee.DateRecord >= firstDate && employee.DateRecord <= secondDate);
				if (inRange)
					Console.WriteLine(employee.ToString());
			}
			Console.ReadKey();
			Console.Clear();
		}

		/// <summary>
		/// Метод записи результатов в файл
		/// </summary>
		/// <param name="path"></param>
		/// <param name="employees"></param>
		public static void SaveAll(string path, Employee[] employees)
		{
			using var strWriter = new StreamWriter(path);
			foreach (var employee in employees)
				strWriter.WriteLine(employee);
		}

		/// <summary>
		/// Метод сортировки массива по возрастанию даты записи
		/// </summary>
		/// <param name="employees"></param>
		public static void Sort(Employee[] employees)
		{
			var dateRecords = new DateTime[employees.Length];
			for (int i = 0; i < employees.Length; i++)
				dateRecords[i] = employees[i].DateRecord;
			Array.Sort(dateRecords, employees);
		}
	}
}



