using System;

namespace _18_Animals.Models
{
	class Bird: IAnimal
	{
		public string AnimalClass => "птица";
		public string Id { get; }
		public string Name { get; }
		public string NickName { get; }
		public string Gender { get; set; }
		public DateTime? BirthDay { get; }
		public Bird(string id, string name, string nickName, string gender, DateTime? birthDay)
		{
			Id = id;
			Name = name;
			NickName = nickName;
			Gender = gender;
			BirthDay = birthDay;
		}
	}
}
