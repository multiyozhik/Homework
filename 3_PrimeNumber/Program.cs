using System;

namespace PrimeNumber
{
	class Program
	{
		static void Main()
		{
			int number;
			while (true)								//цикл для корректного ввода положительного числа
			{
				Console.WriteLine("Введите целое положительное число");
				string input = Console.ReadLine();
				bool isNumber = int.TryParse(input, out number);
				if (isNumber && number > 0) break;
			}
			int divider = 2;							//делитель (делить начинаем с 2)
			bool isPrime = (number != 1);               //если число 1, то isPrime=false, в цикл не заходит - вывод, что непростое
														//если не равно 1, то по умолчанию считаем, что isPrime=true
			while (divider < number)                    //на само число не делим
			{
				if (number % divider == 0)				//если нацело делится, то непростое и выход из цикла
				{
					isPrime = false; 
					break;
				}
				else
				{
					divider++;                          //если не делится нацело, увеличиваем делитель
				}
			}
			string output = isPrime ? $"Число {number} простое" : $"Число {number} непростое";
			Console.WriteLine(output);
			Console.ReadLine();
		}
	}
}
