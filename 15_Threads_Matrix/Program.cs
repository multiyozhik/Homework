// Программа перемножения маленьких матриц (проверка корректности расчета)
// matrix1[n,m] * matrix2[m,k] = matrix3[n,k]

int n = 1000, m = 2000, k = 3000; // [2,3] * [3,4] = [2,4]

int[,] matrix1 = new int[n, m];
int[,] matrix2 = new int[m, k];
int[,] matrix3 = new int[n, k]; // матрица - результат

Random rand = new Random();
int maxNumber = 3; // заполняем матрицы числами 0, 1, 2

Create(ref matrix1); 
Create(ref matrix2); 

MultipleMatrixes();
Console.WriteLine("Задача выполнена"); // в отладчике можно посмотреть matrix3, считает за почти 2 мин.
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
	for (int i = 0; i < n; i++)
		for (int j = 0; j < k; j++)
			matrix3[i, j] = Sum(i, j);

	int Sum(int row, int column)
	{
		int sum = 0;
		for (int l = 0; l < m; l++)
			sum += matrix1[row, l] * matrix2[l, column];
		return sum;
	}
}

void Print(ref int[,] matrix)
{
	int rows = matrix.GetUpperBound(0) + 1; 
	int columns = matrix.Length / (matrix.GetUpperBound(0) + 1);
	Console.WriteLine($"matrix  {rows}*{columns}");
	for (int i = 0; i < rows; i++)
	{
		for (int j = 0; j < columns; j++)
			Console.Write($"{matrix[i, j]}\t");
		Console.WriteLine();
	}
	Console.ReadLine();
}

