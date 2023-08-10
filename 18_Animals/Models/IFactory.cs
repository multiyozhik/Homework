using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _18_Animals.Models
{
	interface IFactory
	{
		public IAnimal Create(string className);
	}
}
