using System;
using System.ComponentModel.Design;
using System.IO;
using System.Threading;

namespace SplitString
{
	class Program
	{

		static void Main()
		{
			Variant1();         //вариант 1 - вручную циклами
			//Variant2();       //вариант 2 - используя встроенный метод Split
		}

		static void Variant1()
		{
			Console.WriteLine("Введите предложение");
			var sentence = Console.ReadLine();
			//Console.WriteLine($"Количество слов в строке {GetCountWords(sentence)}");
			var words = SplitToWords(sentence);     // метод разделения строки на слова
			PrintWords(words);


			//метод возвращает количество слов в строке в общем случае (например, несколько подряд пробелов или начинается с пробела или заканчивается пробелом
			//static int GetCountWords(string inputString)
			//{
			//	int wordsCount = 1;									// случай, когда все пробелы и нет символов, не рассматривается
			//	for (int i = 0; i < inputString.Length; i++)
			//	{
			//		if (inputString[i] == ' ')                      // если i-й символ пробел, при этом
			//			&& (i != 0) && (i != inputString.Length-1)  // пробел не первый и не последний в строке
			//			&& !(inputString[i-1] == ' ')			    // предыдущий символ не пробел (т.е. не два подряд пробела)
			//		{
			//			wordsCount++;                               // счетчик слов увеличивается
			//		}
			//	}
			//	return wordsCount;      
			//}

			//метод возвращает количество слов, когда каждое слово в предложении отделено одним пробелом
			static int GetCountWords(string inputString)
			{
				int wordsCount = 1;
				foreach (char symbol in inputString)
				{
					if (symbol == ' ')
						wordsCount++;
				}
				return wordsCount;
			}

			// метод возвращает подстроку по начальному и конечному индексу в строке
			static string GetSubstring(string inputString, int indexStart, int indexEnd)
			{
				string substring = "";
				for (int i = indexStart; i <= indexEnd; i++)
					substring += inputString[i];
				return substring;
			}

			// метод возвращает массив строк (слов)
			static string[] SplitToWords(string inputString)
			{
				int countWords = GetCountWords(inputString);            // получаем количество слов
				var words = new string[countWords];                     // создаем массив типа string количеством слов
				int indexStart = 0;                                     
				int k = 0;                                              // счетчик слов 
				for (int i = 0; i < inputString.Length; i++)            // цикл по символам строки
				{
					if (inputString[i] == ' ')                                      // если дошли до пробела
					{
						int indexEnd = i - 1;                                       // индекс конца слова = предыдущий символ перед пробелом
						words[k] = GetSubstring(inputString, indexStart, indexEnd); // выделяем слово
						indexStart = i + 1;                                         // индекс начала слова указываем на следующий символ после пробела
						k++;                                                        // счетчик++ для записи следующего слова
					}
				}
				words[k] = GetSubstring(inputString, indexStart, inputString.Length - 1); //последнее слово до конца строки
				return words;
			}
		}

		// метод печати слов (вывод по элементам массива слов)
		static void PrintWords(string[] arrayWords)
		{
			foreach (var word in arrayWords)
			{
				Console.WriteLine(word);
			}
			Console.ReadKey();
		}

		static void Variant2()
		{
			Console.WriteLine("Введите предложение");
			var sentence = Console.ReadLine();
			var subString = sentence.Split(' ');
			PrintWords(subString);
		}
	}
}

