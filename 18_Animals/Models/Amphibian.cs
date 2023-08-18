using System;

namespace _18_Animals.Models
{
	class Amphibian : IAnimal
	{
		public string AnimalClass => "земноводное";
		public string Id { get; }
		public string Name { get; }
		public string NickName { get; }
		public string Gender { get; set; }
		public DateTime? BirthDay { get; }
		public Amphibian(string id, string name, string nickName, string gender, DateTime? birthDay)
		{
			Id = id; 
			Name = name; 
			NickName = nickName; 
			Gender = gender;
			BirthDay = birthDay;
		}
	}
}
