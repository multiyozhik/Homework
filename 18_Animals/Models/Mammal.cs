using System;

namespace _18_Animals.Models
{
	class Mammal : IAnimal
	{
		public string? AnimalClass => "млекопитающее";
		public string? Id { get; }
		public string? AnimalSpecies { get; }
		public string? NickName { get; }
		public string? Gender { get; set; }
		public DateTime? BirthDay { get; }
		public Mammal(string id, string animalSpecies, string nickName, string gender, DateTime? birthDay)
		{
			Id = id;
			AnimalSpecies = animalSpecies;
			NickName = nickName;
			Gender = gender;
			BirthDay = birthDay;
		}
	}
}
