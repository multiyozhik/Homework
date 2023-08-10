using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _18_Animals.Models
{
	class Repository : IRepository //конкретная реализация репозитория и CRUD-операции в нем
	{
		readonly List<IAnimal> AnimalsList;

		public void Add(IAnimal animal) => AnimalsList.Add(animal);
		
		public List<IAnimal> GetAnimalsList() => AnimalsList;		

		public void Remove(IAnimal animal) => AnimalsList.Remove(animal);		

		public void Save(ISaver saver) //в конструктор передаем конкретный способ сохранения типа ISaver (с методом Save)
		{
			saver.Save(AnimalsList);
		}

		public Repository(IEnumerable<IAnimal> animalsList)
		{
			AnimalsList = animalsList.ToList();
		}
	}
}
