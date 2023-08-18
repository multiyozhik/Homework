using System;

namespace _18_Animals.Models
{
	public interface IAnimal
	{
		string? AnimalClass { get; }
		string? Id { get; }
		string? AnimalSpecies { get; }
		string? NickName { get; }
		string? Gender { get; set; }
		DateTime? BirthDay { get; }
	}	
}
