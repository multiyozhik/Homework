﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _18_Animals.Models
{
	//record (по сути это record class ссылочный, уже под капотом с-во Name и конструктор с инициал. Name)
	record Serpentium(string Name) : IAnimal 
	{
		public string ClassName => "пресмыкающееся";
	}
}
