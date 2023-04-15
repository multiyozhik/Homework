// Вычислить кол-во чисел на промежутке от 1 000 000 000 до 2 000 000 000,
// в кот. сумма цифр числа кратна последней цифре числа

int minNumber = 1000000000;
int maxNumber = 2000000000;

int count = 0;			// счетчик
Parallel.For(
	minNumber, 
	maxNumber,
	new ParallelOptions { MaxDegreeOfParallelism = 4}, 
	DigitalsSumIsMultiple); // т.к. 4-ядерный процессор

Console.WriteLine(count);
Console.ReadLine();

void DigitalsSumIsMultiple(int number)
{
	int lastDigit = number % 10;
	int sum = 0;
	while (number != 0)
	{
		sum += number % 10;
		number /= 10;
	}
	bool isMultiple = lastDigit != 0 && sum % lastDigit == 0;
	if (isMultiple) Interlocked.Increment(ref count); // можно испол. lock(), но здесь удобнее Interlocked
}
// в рез. 282млн. получается, у меня чуть больше 1 мин. считает
