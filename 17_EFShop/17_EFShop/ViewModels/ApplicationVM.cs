using _17_EFShop.Views;
using System.Linq;

namespace _17_EFShop.ViewModels
{
	class ApplicationVM
	{
		private readonly RelayCommand? loadClientsDbCommand;
		public RelayCommand LoadClientsDbCommand
		{
			//Оператор null-объединения ?? возвращает левый операнд, если этот операнд не равен null, иначе-правый операнд
			get => loadClientsDbCommand ?? new RelayCommand(
				obj =>
					{
						using (var dbContext = new ApplicationDbContext())
						{
							var clientsWindow = new ClientsWindow()
							{
								DataContext = new ClientsDbVM(dbContext)
								{
									ClientsAccount = dbContext.Clients.Count()
								}
							};
							clientsWindow.ShowDialog();
						}
					}
				);
		}

		private readonly RelayCommand? loadShoppingsDbCommand;
		public RelayCommand LoadShoppingsDbCommand
		{
			get => loadShoppingsDbCommand ?? new RelayCommand(
				obj =>
					{
						using (var dbContext = new ApplicationDbContext())
						{
							var shoppingsWindow = new ShoppingsWindow()
							{
								DataContext = new ShoppingsDbVm(dbContext)
							};
							shoppingsWindow.ShowDialog();
						}
					});
		}
	}
}
