using System;

namespace MinItem
{
	class Program
	{
		static void Main()
		{
			Console.WriteLine("Введите количество элементов в последовательности");
			var amountOfElements = int.Parse(Console.ReadLine());	//ввод пользователем количества элементов
			var array = new int[amountOfElements];						//создание массива с заданным количеством элементов
			for (int i = 0; i < amountOfElements; i++)
			{
				Console.WriteLine($"Введите {i+1}-й элемент (целое число)");
				string value = Console.ReadLine();						//ввод пользователем элементов массива
				array[i] = int.Parse(value);
			}
			var min = int.MaxValue;										//инициализация минимального эл-та (максимальное для Int32)
			foreach (var item in array)								//цикл по элементам массива
			{
				if (item < min) min = item;
			}
			Console.WriteLine($"Минимальное число в последовательности {min}");
			Console.ReadKey();
		}
	}
}
