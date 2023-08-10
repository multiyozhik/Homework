using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _18_Animals.Models
{
	record Mammal(string Name) : IAnimal
	{
		public string ClassName { get => "млекопитающее"; }
	}
}
