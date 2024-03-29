﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xml.Linq;

namespace _18_Animals.Models
{
	class Fish : IAnimal
	{
		public string? AnimalClass => "рыба";
		public string? Id { get; }
		public string? AnimalSpecies { get; }
		public string? NickName { get; }
		public string? Gender { get; set; }
		public DateTime? BirthDay { get; }
		public Fish(string id, string animalSpecies, string nickName, string gender, DateTime? birthDay)
		{
			Id = id;
			AnimalSpecies = animalSpecies;
			NickName = nickName;
			Gender = gender;
			BirthDay = birthDay;
		}
	}
}
