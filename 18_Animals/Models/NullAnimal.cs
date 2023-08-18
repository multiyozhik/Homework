using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _18_Animals.Models
{
	internal class NullAnimal:IAnimal
	{
		public string? AnimalClass => "Не определен";
		public string? Id { get => "Не определен"; }
		public string? AnimalSpecies { get => "Не определен"; }
		public string? NickName { get => "Не определен"; }
		public string? Gender { get => "Не определен"; set { } }
		public DateTime? BirthDay { get => DateTime.Today; }		
	}
}
