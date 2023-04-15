using System;

namespace MatrixSum
{
	class Program
	{
		static void Main()
		{
			Console.WriteLine("Введите количество строк");
			var rows = Convert.ToInt32(Console.ReadLine());			//ввод количества строк
			Console.WriteLine("Введите количество столбцов");
			var columns = Convert.ToInt32(Console.ReadLine());      //ввод количества столбцов
			var matrix = new int[rows, columns];
			var random = new Random();
			int sum = 0;											//инициализация переменной суммы
			for (int i = 0; i < rows; i++)							//цикл по строкам
			{
				for (int j = 0; j < columns; j++)					//внутри цикл по столбцам
				{
					var valueRandom = random.Next(10000); //если не ограничить диапазон, может быть 
																	//переполнение Int32 (сумма отрицательная)
					matrix[i, j] = valueRandom;						
					sum += valueRandom;
					Console.Write($"{valueRandom}\t");
				}
				Console.WriteLine();
			}
			Console.WriteLine($"Сумма элементов матрицы {sum}");
			Console.ReadKey();
		}
	}
}
