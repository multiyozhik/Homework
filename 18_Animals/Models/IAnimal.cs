using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _18_Animals.Models
{
	//конкретные реализации IAnimal - это классы Serpentium (пресмык.), Amphibian (земновод.), Mammalian (млекоп.)
	interface IAnimal
	{
		public string ClassName { get; } //пресмыкающееся, земноводное или млекопитающее
		public string Name { get; } //конкретное название животного (рандомно из списка или новое неизвестное животное)		
	}
}
