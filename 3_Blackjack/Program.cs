using System;
using System.Globalization;

namespace Blackjack
{
	class Program
	{
		static void Main()
		{
			Console.WriteLine("Поиграем в Blackjack!");
			int number;											   //количество карт
			while (true)										   //бесконечный цикл, пока не будет корректно введено число
			{
				Console.WriteLine("Введите количество карт (целое положительное число)");
				string input = Console.ReadLine();
				bool nIsInteger = int.TryParse(input, out number); //TryParse возвращает успешно ли преобразование в number 
				if (nIsInteger && number > 0) break;			   // положительное целое число -> ок, выходим из цикла
			}
			int sum = 0;                                           //инициализируем сумму 
			Console.WriteLine("При указании номинала карты Валет = J, Дама = Q, Король = K, Туз = T");
			for (int i = 1; i <= number;)                         //цикл по картам, счетчик i будет меняться дальше внутри switch
			{
				Console.WriteLine($"Введите номинал {i}-й карты");
				string inputValue = Console.ReadLine();
				switch (inputValue)                       //string inputValue - номинал карты
				{
					case "J":                             //для карт "J", "Q", "K", "T" - сумма увеличивается на 10
					case "Q":
					case "K":
					case "T":
						sum += 10;
						i++;                              //для перехода к следующей карте
						break;                            //выход из switch
					case "2":                             //для карт "2"..."10" - берем их значение, используя Parse
					case "3":
					case "4":
					case "5":
					case "6":
					case "7":
					case "8":
					case "9":
					case "10":
						int value = int.Parse(inputValue);
						sum += value;                      //сумму увеличиваем на значение номинала карты
						i++;                               //для перехода к следующей карте
						break;                             //выход из switch
					default:
						Console.WriteLine($"Некорректный ввод номинала {i} карты");
						break;                             //к следующей карте не переходим, i остается тот же самый
														   //опять по циклу запрос номинала, пока не будет корректно введен
				}
			}
			Console.WriteLine($"Сумма {number} карт равна {sum}");
			Console.ReadKey();
		}
	}
}
