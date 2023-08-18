using System;

namespace _18_Animals.Models
{
	class Amphibian : IAnimal
	{
		public string? AnimalClass => "земноводное";
		public string? Id { get; }
		public string? AnimalSpecies { get; }
		public string? NickName { get; }
		public string? Gender { get; set; }
		public DateTime? BirthDay { get; }
		public Amphibian(string id, string animalSpecies, string nickName, string gender, DateTime? birthDay)
		{
			Id = id; 
			AnimalSpecies = animalSpecies; 
			NickName = nickName; 
			Gender = gender;
			BirthDay = birthDay;
		}
	}
}
