using System.Collections.Generic;
using System.Linq;

namespace _18_Animals.Models
{
	internal class Model
	{
		public Model()
		{

		}

		public List<IAnimal> AnimalsList { get; } = new List<IAnimal>();

		public IAnimal Add(AnimalData animalData)
		{
			IAnimal newAnimal = Factory.Create(animalData);
			AnimalsList.Add(newAnimal);
			return newAnimal;
		}

		public void Change(IAnimal selectedAnimal, string newGender)
		{
			var selectedAnimalIndex = AnimalsList.IndexOf(
				AnimalsList.Where(animal => animal.Id == selectedAnimal.Id).First());
			AnimalsList[selectedAnimalIndex].Gender = newGender;
		}

		public void Remove(IAnimal animal) => AnimalsList.Remove(animal);

		public void Export(string saveFormat)
		{
			IExport export = saveFormat switch
			{
				"txt" => new ExportToTxt(),
				"csv" => new ExportToCsv()
			};
			export.Export(AnimalsList);
		}
	}
}
