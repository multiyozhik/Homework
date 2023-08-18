using System;

namespace _18_Animals.Models
{
	static class Factory
	{
		//фабрика генерит конкретные реализации типа IAnimal по заданному классу животного (тут полиморфизм)
		public static IAnimal Create(AnimalData animalData)
		{
			var id = Guid.NewGuid().ToString();
			return animalData switch
			{
				var (className, name, nickName, gender, birthDay) => className switch
				{
					"пресмыкающееся" => new Serpentium(id, name, nickName, gender, birthDay),
					"земноводное" => new Amphibian(id, name, nickName, gender, birthDay),
					"млекопитающее" => new Mammal(id, name, nickName, gender, birthDay),
					"птица" => new Bird(id, name, nickName, gender, birthDay),
					"рыба" => new Fish(id, name, nickName, gender, birthDay)
				}
			};
		}
	}
}
