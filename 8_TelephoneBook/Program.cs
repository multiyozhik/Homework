using System;
using System.Collections.Generic;

namespace TelephoneBook
{
	class Program
	{
		static void Main()
		{
			var phoneBook = FillPhoneBook();
			Console.WriteLine("Введите номер телефона для поиска его владельца");
			string phoneNumber = Console.ReadLine();
			FindPerson(phoneBook, phoneNumber);
		}

		static void FindPerson(Dictionary<string, string> phoneBook, string phoneNumber)
		{
			bool isFound = phoneBook.TryGetValue(phoneNumber, out string phoneOwnerFullName);
			var message = isFound
				? phoneOwnerFullName 
				:$"Владельца номера телефона {phoneNumber} не зарегистрировано";
			Console.WriteLine(message);
			Console.ReadKey();
		}

		static Dictionary<string, string> FillPhoneBook()
		{
			var phoneBook = new Dictionary<string, string>();
			while (AppendPhoneNumber(phoneBook))
				continue;
			return phoneBook;
		}

		static bool AppendPhoneNumber(Dictionary<string, string> phoneBook)
		{
			Console.WriteLine("Введите номер телефона. Если ввод телефонов закончен, нажмите Enter");
			var phoneNumber = Console.ReadLine();
			if (phoneNumber == "")
				return false;
			Console.WriteLine($"Введите Ф.И.О. владельца телефона с номером {phoneNumber}");
			var phoneOwner = Console.ReadLine();
			phoneBook.Add(phoneNumber, phoneOwner);
			return true;
		}
	}
}
