using System;
using System.Collections.Generic;

namespace RepeatCheck
{
	class Program
	{
		static void Main()
		{
			var numbersSet = FillNumbersSet();
		}

		static HashSet<int> FillNumbersSet()
		{
			var numbersSet = new HashSet<int>();
			while (true)
			{
				Console.WriteLine("Введите число");
				var value = Console.ReadLine();
				if (value == "")
					break;
				var number = int.Parse(value);
				if (numbersSet.Contains(number))
					Console.WriteLine($"Число {number} уже вводилось ранее");
				else
				{
					numbersSet.Add(number);
					Console.WriteLine($"Число {number} успешно сохранено");
				}
			}
			Console.ReadKey();
			return numbersSet;
		}
	}
}
