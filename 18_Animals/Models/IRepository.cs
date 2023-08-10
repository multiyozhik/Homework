using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _18_Animals.Models
{
	interface IRepository
	{				
		//IGetterInfo создать в зависимости от того, в каком виде вывести информацию в ISaver
		List<IAnimal> GetAnimalsList(); // получить данные по животным (на консоль, в wpf, в winforms?)		
		void Add(IAnimal animal);  //добавить новое (неизвестное) животное в список животных
		//void Change(IAnimal animal); //изменить какие-то параметры в животном
		void Remove(IAnimal animal); //удалить животное из списка (например, исчезло с планеты)		
		void Save(ISaver saveFormat); //сохранить данные в разных форматах (txt, csv, pdf) 
	}
}
