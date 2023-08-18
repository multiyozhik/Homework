using System;

namespace _18_Animals.Views
{
	internal interface IAnimalCardView
	{
		public string? AnimalClass { get; }

		public string? NickName { get; }

		public string? AnimalSpecies { get; }

		public string? Gender { get; set; }
		
		public DateTime? BirthDay { get; }

		public void SaveAnimalCard();
	}
}