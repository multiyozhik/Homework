using System;

namespace GuessNumber
{
	class Program
	{
		static void Main()
		{
			int maxNumber;															//объявление переменной - максимальное число
			while (true)                                                            //ввод с консоли макс.числа
			{
				Console.WriteLine("Введите максимальное число диапазона");
				bool maxParse = int.TryParse(Console.ReadLine(), out maxNumber);	
				if (maxParse) break;
			}
			Console.WriteLine("Угадай число!");
			var random = new Random();									//объявление переменной класса Random
			var unknownNumber = random.Next(maxNumber + 1);   //диапазон от 0 до maxNumber включительно
			string output;												//переменная для вывода результата
			while (true)
			{
				Console.WriteLine("Введи число");						
				var input = Console.ReadLine();                   //ввод числа пользователем
				if (string.IsNullOrEmpty(input))						//проверка пустая ли строка (тогда выход из цикла)
				{
					output = $"Загаданное число {unknownNumber}";
					break;
				}
				var number = int.Parse(input);
				if (number == unknownNumber)							//если угадал - выход из цикла
				{
					output = "Угадал!";
					break;
				}
				output = (number < unknownNumber) ? "Число меньше загаданного" : "Число больше загаданного";
				Console.WriteLine(output);								//не угадал - подсказка и дальше по циклу
			}
			Console.WriteLine(output);					
			Console.ReadKey();
		}
	}
}
