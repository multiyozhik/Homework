// Программа перемножения больших матриц 
// matrix1[n,m] * matrix2[m,k] = matrix3[n,k]

int n = 1000, m = 2000, k = 3000; // [1000,2000] * [2000,3000] = [1000,3000]

int[,] matrix1 = new int[n, m];
int[,] matrix2 = new int[m, k];
int[,] matrix3 = new int[n, k]; // матрица - результат

Random rand = new Random();
int maxNumber = 3; // заполняем матрицы числами 0, 1, 2

Parallel.Invoke(() => Create(ref matrix1), () => Create(ref matrix2));

MultipleMatrixes();

Console.WriteLine("Задача выполнена"); // в отладчике можно посмотреть matrix3, считает за 35 сек., однопоточно 1мин.15сек.
Console.ReadLine();

//Print(ref matrix1); // ref чтобы не копировать огромную матрицу, а передать ссылку на нее и сэкономить ресурсы
//Print(ref matrix2);
//Print(ref matrix3);

void Create(ref int[,] matrix)
{
	int rows = matrix.GetUpperBound(0) + 1; // GetUpperBound(номер измерения) - это индекс посл.эл-та
	int columns = matrix.Length / (matrix.GetUpperBound(0) + 1);
	for (int i = 0; i < rows; i++)
		for (int j = 0; j < columns; j++)
			matrix[i, j] = rand.Next(maxNumber);
}

void MultipleMatrixes()
{
	List<Task> tasks = new List<Task>();
	for (int i = 0; i < n; i++)
	{
		var r = i; // если task запускать внутри циклов for по i и по j, то IndexOutException, поэтому делаем более крупные задачи и через локал.переменную
		tasks.Add(Task.Run(() =>
		{
			for (int j = 0; j < k; j++)
				Sum(r, j);
		}));
	}	
	Task.WaitAll(tasks.ToArray());

	void Sum(int row, int column)
	{
		int sum = 0;
		for (int l = 0; l < m; l++)
			sum += matrix1[row, l] * matrix2[l, column];
		matrix3[row, column] = sum;
	}
}

//void Print(ref int[,] matrix)
//{
//	int rows = matrix.GetUpperBound(0) + 1;
//	int columns = matrix.Length / (matrix.GetUpperBound(0) + 1);
//	Console.WriteLine($"matrix  {rows}*{columns}");
//	for (int i = 0; i < rows; i++)
//	{
//		for (int j = 0; j < columns; j++)
//			Console.Write($"{matrix[i, j]}\t");
//		Console.WriteLine();
//	}
//	Console.ReadLine();
//}
