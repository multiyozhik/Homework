using System;
using System.Collections.Generic;

namespace Collections
{
	class Program
	{
		static void Main()
		{
			var numbersAmount = 100;
			var maxValue = 100;
			var intNumbersList = FillList(numbersAmount, maxValue);
			Console.WriteLine("Исходный список чисел");
			Print(intNumbersList);
			int firstNumber = 25;
			int secondNumber = 50;
			var listAfterRemove = RemoveSomeNumbers(intNumbersList, firstNumber, secondNumber);
			Console.WriteLine($"Список чисел после удаления чисел от {firstNumber} до {secondNumber} включительно");
			Print(listAfterRemove);
		}

		/// <summary>
		/// Метод возвращает список чисел после удаления из него чисел больше firstNumber, но меньше secondNumber включительно
		/// </summary>
		/// <param name="numbersList"></param>
		/// <param name="firstNumber"></param>
		/// <param name="secondNumber"></param>
		/// <returns></returns>
		static List<int> RemoveSomeNumbers(List<int> numbersList, int firstNumber, int secondNumber)
		{
			var numbersArray = numbersList.ToArray();
			foreach (var value in numbersArray)
			{
				if (value >= firstNumber && value <= secondNumber)
					numbersList.Remove(value);
			}
			return numbersList;
		}

		
		/// <summary>
		/// Метод вывода списка на консоль
		/// </summary>
		/// <param name="numbersList"></param>
		static void Print(List<int> numbersList)		
		{
			foreach (var value in numbersList)
			{
				Console.Write(value + "\t");
			}
			Console.WriteLine();
			Console.ReadKey();
		}


		/// <summary>
		/// Метод возвращает список, заполненный случайными целыми числами от 0 до maxValue (не включая maxValue)
		/// </summary>
		/// <param name="numberAmount"></param>
		/// <param name="maxValue"></param>
		/// <returns></returns>
		static List<int> FillList(int numberAmount, int maxValue)		
		{
			var intNumbersList = new List<int>();
			var random = new Random();
			for (int i = 0; i < numberAmount; i++)
				intNumbersList.Add(random.Next(maxValue));
			return intNumbersList;
		}
	}
}
