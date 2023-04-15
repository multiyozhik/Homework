using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Employees
{

	/// <summary>
	/// Структура - запись по сотруднику
	/// </summary>
	public struct Employee
	{
		public int Id { get; set; }
		public DateTime DateRecord { get; set; }
		public string FullName { get; set; }
		public int Age { get; set; }
		public int Height { get; set; }
		public DateTime BithDay { get; set; }
		public string BithPlace { get; set; }

		// конструктор структуры - записи по сотруднику
		public Employee(int id, DateTime dateRecord, string fullName, int age, int height, DateTime bithDay, string bithPlace)
		{
			Id = id;
			DateRecord = dateRecord;
			FullName = fullName;
			Age = age;
			Height = height;
			BithDay = bithDay;
			BithPlace = bithPlace;
		}

		/// <summary>
		/// Вывод сведений на консоль по сотруднику
		/// </summary>
		public void Show()
		{
			Console.WriteLine($"\nID сотрудника {Id}" +
							  $"\nДата и время добавления записи {DateRecord}" +
							  $"\nФ.И.О. {FullName}" +
							  $"\nВозраст {Age}" +
							  $"\nРост {Height}" +
							  $"\nДата рождения {BithDay}" +
							  $"\nМесто рождения {BithPlace}");
			Console.ReadKey();
		}

		/// <summary>
		/// Строковое выражение записи сотрудника с учетом разделителя # (переопределение метода ToString)
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return String.Join('#', Id, DateRecord, FullName, Age, Height, BithDay, BithPlace);
		}

	}
}
