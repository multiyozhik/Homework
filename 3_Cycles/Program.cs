using System;

namespace Cycles
{
	class Program
	{
		static void Main()
		{
			//вариант 1 - без проверки корректности ввода
			//Console.WriteLine("Введите целое число");
			//string input = Console.ReadLine();
			//int number = int.Parse(input);				// можно int number = Convert.ToInt32(input);
			//var output = number % 2 == 0                  // здесь можно также явно string output
			//	? "Число четное"
			//	: "Число нечетное";
			//Console.WriteLine(output);
			//Console.ReadKey();

			//вариант 2 - с проверкой корректности ввода
			Console.WriteLine("Введите целое число");
			string input = Console.ReadLine();
			bool isNumber = int.TryParse(input, out int number); // проверка преобразования строки в целое число
			if (isNumber)                                       // если true (TryParse успешно преобразовал и присвоил number)
			{
				var output = number % 2 == 0        // тернарный оператор (проверка -делится ли число нацело без остатка
					? "Число четное"
					: "Число нечетное";
				Console.WriteLine(output);
			}
			else
			{
				Console.WriteLine("Ошибка ввода");      // если false (TryParse не смог преобразовать в целое число, Null и т.п. )
			}
			Console.ReadKey();
		}
	}
}
