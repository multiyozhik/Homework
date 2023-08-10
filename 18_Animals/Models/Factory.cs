using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _18_Animals.Models
{
	class Factory : IFactory //конкретная реализация ICreator
	{
		Random random = new();
		List<string> classNamesList = new() { "пресмыкающееся", "земноводное", "млекопитающее" };
		List<string> serpentiumsNamesList = new() { "змея", "ящерица", "хамелеон", "игуана", "варан", "крокодил", "черепаха", "гаттерия" };
		List<string> amphibiansNamesList = new() { "лягушка", "жаба", "тритон", "саламандра", "червяга", "ацелот" };
		List<string> mammalsNamesList = new() { "ехидна", "опоссум", "летучая мышь", "крот", "обезьяна", "дельфин", "заяц" };
		public IAnimal Create(string className) => className switch
		{
			"пресмыкающееся" => new Serpentium(serpentiumsNamesList[random.Next(serpentiumsNamesList.Count)]),
			"земноводное" => new Amphibian(amphibiansNamesList[random.Next(amphibiansNamesList.Count)]),
			"млекопитающее" => new Mammal(mammalsNamesList[random.Next(mammalsNamesList.Count)])
		};
	}
}
