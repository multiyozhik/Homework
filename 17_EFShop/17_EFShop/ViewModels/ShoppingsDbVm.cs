using _17_EFShop.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace _17_EFShop.ViewModels
{
	//ShoppingsDbVm используется в качестве DataContext окна ShoppingsWindow
	class ShoppingsDbVm : INotifyPropertyChanged //реализ. интерфейс для обновления визульной части при изм. ShoppingsList
	{
		private readonly ApplicationDbContext dbContext;


		private List<Shopping>? shoppingsList;
		public List<Shopping>? ShoppingsList //отслеживаем с-во списка покупок,
											 //при каждом изменении в setter вызываем PropertyChanged
		{
			get => shoppingsList;
			set
			{
				shoppingsList = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ShoppingsList)));
			}
		}
		public event PropertyChangedEventHandler? PropertyChanged;

		public ShoppingsDbVm(ApplicationDbContext dbContext)
		{
			this.dbContext = dbContext;
			ShoppingsList = this.dbContext.Shoppings.ToList();
		}
	}
}
