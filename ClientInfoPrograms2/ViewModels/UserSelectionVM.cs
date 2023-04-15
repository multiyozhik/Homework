using ClientsInfoProgram.Models;
using ClientsInfoProgram.Services;
using ClientsInfoProgram.Views;
using System.Collections.ObjectModel;
using System.Linq;
using System.Collections.Specialized;


namespace ClientsInfoProgram.ViewModels
{
	class UserSelectionVM
	{
		public IUserType[] UserTypes { get; } = new IUserType[] { new Consultant(), new Manager() };
		IDepartmentRepository DepatmentsRepository { get; } = new DepatmentsRepository();
		IClientRepository ClientsRepository { get; } = new ClientsRepository();
		IUserType _selectedUserType;
		public IUserType SelectedUserType
		{
			get => _selectedUserType;
			set
			{
				_selectedUserType = value;
				var clientsInfoWindow = new ClientsInfoWindow();
				var departmentsList = new ObservableCollection<Department>(DepatmentsRepository.GetDepartmets());
				var clientsList = new ObservableCollection<ClientVM>(
						ClientsRepository.GetClients().Select(ConvertClientToVM));
				clientsList.CollectionChanged += ClientsList_CollectionChanged;

				clientsInfoWindow.DataContext = new ClientsInfoVM()
				{
					ClientsList = clientsList,
					DepartmentsList = departmentsList,
					IsReadOnly = value is not Manager,
					IsCanAddNewClient = value is Manager,
					ClientsRepository = ClientsRepository
				};

				clientsInfoWindow.ShowDialog();
			}
		}

		private void ClientsList_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			if (e.Action == NotifyCollectionChangedAction.Add)
			{	
				foreach (ClientVM item in e.NewItems)
				{
					item.Changer = SelectedUserType;
					item.HandlePropertyChanges(nameof(item.Changer), "добавление", "", "");
				}
			}
		}

		ClientVM ConvertClientToVM(Client client)
		{
			return new ClientVM()
			{
				Changer = SelectedUserType,
				DepartmentID = client.DepartmentId,
				LastName = client.LastName,
				FirstName = client.FirstName,
				MiddleName = client.MiddleName,
				PhoneNumber = client.PhoneNumber,
				Passport = client.Passport,
				ChangingTime = client.ChangingTime,
				ChangedData = client.ChangedData,
				ChangingType = client.ChangingType,
				LastChanger = client.LastChanger
			};
		}
	}
}




