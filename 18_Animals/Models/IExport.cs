using System.Collections.Generic;

namespace _18_Animals.Models
{
	interface IExport
	{		
		string SaveFormat { get; }
		void Export(List<IAnimal> animalsList);
	}
}
