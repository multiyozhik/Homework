using _17_EFShop.Models;
using _17_EFShop.Views;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace _17_EFShop.ViewModels
{
	//делаем DataContext окна ClientsWindow, уведомляющим об изменении с-ва ClientsList (при добавлении AddedClient и т.п.)
	class ClientsDbVM : INotifyPropertyChanged
	{
		private readonly ApplicationDbContext dbContext;

		private int clientsAccount;
		public int ClientsAccount       //отслеживаем кол-во клиентов
		{
			get => clientsAccount;
			set
			{
				clientsAccount = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ClientsAccount)));//вызываем PropertyChanged
			}
		}

		private List<Client>? clientsList;
		public List<Client>? ClientsList //отслеживаем изменения коллекции
		{
			get => clientsList;
			set
			{
				clientsList = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ClientsList)));//вызываем PropertyChanged
			}
		}

		private Client selectedClient;
		public Client SelectedClient //отслеживаем изменение выделенного клиента
		{
			get => selectedClient;
			set
			{
				selectedClient = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedClient)));
			}
		}
		//private bool doneBtnIsVisible;
		//public bool DoneBtnIsVisible => (AddedClient is not null) ? String.IsNullOrEmpty(AddedClient.Error) : false;



		public event PropertyChangedEventHandler? PropertyChanged;//реализуем INotifyPropertyChanged

		public ClientsDbVM(ApplicationDbContext dbContext)
		{
			this.dbContext = dbContext;
			ClientsList = dbContext.Clients.ToList();
		}
		public Client? AddedClient { get; set; }

		private readonly RelayCommand? addClientCommand;
		public RelayCommand AddClientCommand                //добавить нового клиента
		{
			get => addClientCommand ?? new RelayCommand(obj =>
				{

					AddedClient = new Client();
					var clientDataWindow = new ClientDataWindow()
					{
						DataContext = AddedClient
					};

					var clientDataWindowResult = clientDataWindow.ShowDialog();
					if (clientDataWindowResult is false) return;

					dbContext.Clients.Add(AddedClient);
					dbContext.SaveChanges();
					ClientsList = dbContext.Clients.ToList(); //по setter вызов PropertyChanged с-ва ClientsList
															  //(визуальная часть, кот. забиндина, будет обновляться)
					ClientsAccount = dbContext.Clients.Count();
				});
		}

		private readonly RelayCommand? changeClientCommand;
		public RelayCommand ChangeClientCommand             //изменить данные выбранного клиента
		{
			get => changeClientCommand ?? new RelayCommand(obj =>
				{
					var clientDataWindow = new ClientDataWindow()
					{
						DataContext = SelectedClient
					};
					var clientDataWindowResult = clientDataWindow.ShowDialog();
					if (clientDataWindowResult is false) return;
					dbContext.Clients.Update(SelectedClient);
					dbContext.SaveChanges();
					ClientsList = dbContext.Clients.ToList();
				});
		}
		private RelayCommand showClientShoppings;
		public RelayCommand ShowClientShoppings             //показать покупки выбранного клиента
		{
			get => showClientShoppings ?? new RelayCommand(obj =>
				{
					if (SelectedClient is null) return;
					var shoppingsWindow = new ShoppingsWindow()
					{
						DataContext = new ShoppingsDbVm(dbContext)
						{
							ShoppingsList = dbContext.Shoppings.Where(
								shopping => shopping.Email == selectedClient.Email).ToList()
						}
					};
					shoppingsWindow.ShowDialog();
				});
		}

		public Shopping NewShopping { get; set; }           //совершить новую покупку для выбранного клиента

		private RelayCommand doShopping;
		public RelayCommand DoShopping
		{
			get => doShopping ?? new RelayCommand(obj =>
			{
				if (SelectedClient is null) return;
				NewShopping = new Shopping();
				NewShopping.Email = selectedClient.Email;
				var shoppingsDataWindow = new ShoppingDataWindow()
				{
					DataContext = NewShopping
				};
				shoppingsDataWindow.ShowDialog();
				if (shoppingsDataWindow.DialogResult is false) return;
				dbContext.Shoppings.Add(NewShopping);
				dbContext.SaveChanges();
				var shoppingWindow = new ShoppingsWindow()
				{
					DataContext = new ShoppingsDbVm(dbContext)
					{
						ShoppingsList = dbContext.Shoppings.ToList()
					}
				};
				shoppingWindow.ShowDialog();
			});
		}

		private RelayCommand deleteClient;
		public RelayCommand DeleteClient                //удалить выбранного клиента и его покупки
		{
			get => deleteClient ?? new RelayCommand(obj =>
			{
				if (SelectedClient is null) return;
				var deletedShoppingsList = dbContext.Shoppings.Where(
					shopping => shopping.Email == SelectedClient.Email).ToList();
				dbContext.Clients.Remove(SelectedClient);
				dbContext.Shoppings.RemoveRange(deletedShoppingsList);
				dbContext.SaveChanges();
				ClientsList = dbContext.Clients.ToList();
				ClientsAccount = dbContext.Clients.Count();
			});
		}

		private RelayCommand clearDb;
		public RelayCommand ClearDb
		{
			get => clearDb ?? new RelayCommand(obj =>
			{						
				dbContext.Database.ExecuteSqlRaw("TRUNCATE TABLE Clients");
				dbContext.Database.ExecuteSqlRaw("TRUNCATE TABLE Shoppings");
				dbContext.SaveChanges();
				ClientsList = dbContext.Clients?.ToList();
				ClientsAccount = dbContext.Clients.Count();
			});
		}
	}
}
