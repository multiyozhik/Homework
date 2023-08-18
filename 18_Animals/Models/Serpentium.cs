using System;

namespace _18_Animals.Models
{
	class Serpentium : IAnimal
	{
		public string? AnimalClass => "пресмыкающееся";
		public string? Id { get; }
		public string? AnimalSpecies { get; }
		public string? NickName { get; }
		public string? Gender { get; set; }
		public DateTime? BirthDay { get; }
		public Serpentium(string id, string animalSpecies, string nickName, string gender, DateTime? birthDay)
		{
			Id = id;
			AnimalSpecies = animalSpecies;
			NickName = nickName;
			Gender = gender;
			BirthDay = birthDay;
		}
	}
}
