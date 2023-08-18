using System.Collections.Generic;
using System.IO;

namespace _18_Animals.Models
{
	class ExportToTxt : IExport
	{	
		public string SaveFormat { get => "txt"; }

		public void Export(List<IAnimal> animalsList)
		{			
			string path = Path.Combine(Directory.GetCurrentDirectory(), "animalList.txt");
			using StreamWriter writer = new(path, false);
			foreach (var animal in animalsList)
			{
				writer.WriteLine(animal.Name);
			}
		}
	}
}
