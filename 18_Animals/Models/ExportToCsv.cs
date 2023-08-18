using System.Collections.Generic;
using System.IO;

namespace _18_Animals.Models
{
	internal class ExportToCsv : IExport
	{
		public string SaveFormat { get => "csv"; }

		public void Export(List<IAnimal> animalsList)
		{
			string path = Path.Combine(Directory.GetCurrentDirectory(), "animalList.csv");
			using StreamWriter writer = new(path, false);
			foreach (var animal in animalsList)
			{
				writer.WriteLine(animal.AnimalSpecies);
			}
		}
	}
}
