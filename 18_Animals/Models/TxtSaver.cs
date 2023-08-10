using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _18_Animals.Models
{
	class TxtSaver : ISaver
	{
		public string SaveFormat { get => "txt"; }

		public void Save(List<IAnimal> animalsList)
		{			
			string path = Path.Combine(Directory.GetCurrentDirectory(),"txtFile.txt");
			using StreamWriter writer = new(path, false);
			foreach (var animal in animalsList)
			{
				writer.WriteLine(animal.Name);
			}
		}
	}
}
