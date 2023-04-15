using System;

namespace ReversWords
{
	class Program
	{
		static void Main()
		{
			Console.WriteLine("Введите предложение (каждое слово раздельно одним пробелом)");
			var sentence = Console.ReadLine();			// ввод
			var words = ReversWords(sentence);			// метод меняет слова в обратном порядке
			Print(words);										// вывод
		}

		private static string[] ReversWords(string sentence)
		{
			var words = sentence.Split(' ');				 // метод разделяет на слова
			int i = 0;
			while (i < words.Length/2)
			{
				var temp = words[i];
				words[i] = words[words.Length - 1 - i];
				words[words.Length - 1 - i] = temp;
				i++;
			}
			return words;
		}
		private static void Print(string[] words)
		{
			foreach (var word in words)
			{
				Console.Write(word + " ");
			}
			Console.ReadKey();
		}
	}
}
