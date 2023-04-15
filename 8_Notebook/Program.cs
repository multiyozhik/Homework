using System;
using System.IO;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Notebook
{
	class Program
	{
		static void Main()
		{
			var noteBook = AskForPersonInfo();
			

			var xNotebook = new XDocument(
				new XElement("Person", new XAttribute("name", noteBook.Name),
				new XElement("Address", 
					new XElement("Street", noteBook.Street),
					new XElement("HouseNumber", noteBook.HouseNumber),
					new XElement("FlatNumber", noteBook.FlatNumber)),
				new XElement("Phones", 
					new XElement("MobilePhone", noteBook.MobilePhone),
					new XElement("FlatPhone", noteBook.FlatNumber))));

			var path = "text.xml";
			using var stream = new FileStream(path, FileMode.Create);
			xNotebook.Save(stream);
		}

		static NoteBook AskForPersonInfo()
		{
			Console.WriteLine("Введите ФИО человека");
			var name = Console.ReadLine();
			Console.WriteLine("Введите название улицы");
			var street = Console.ReadLine();
			Console.WriteLine("Введите номер дома");
			var houseNumber = Console.ReadLine();
			Console.WriteLine("Введите номер квартиры");
			var flatNumber = Console.ReadLine();
			Console.WriteLine("Введите номер мобильного телефона");
			var mobilePhone = Console.ReadLine();
			Console.WriteLine("Введите номер домашнего телефона");
			var flatPhone = Console.ReadLine();
			return  new NoteBook(name, street, houseNumber, flatNumber, mobilePhone, flatPhone);
		}
	}
}
