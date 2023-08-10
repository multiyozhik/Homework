using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _18_Animals.Models
{
	interface ISaver
	{
		public string SaveFormat { get; }
		void Save(List<IAnimal> animalsList);
	}
}
