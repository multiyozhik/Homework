using ClientsInfoProgram.Models;
using ClientsInfoProgram.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace ClientsInfoProgram.ViewModels
{
	class ClientsInfoVM : INotifyPropertyChanged
	{
		public ObservableCollection<Department> DepartmentsList { get; set; }
		ObservableCollection<ClientVM> _displayedClientsList;
		public ObservableCollection<ClientVM> DisplayedClientsList
		{
			get => _displayedClientsList;
			set
			{
				_displayedClientsList = value;
				PropertyChanged(this, new PropertyChangedEventArgs(nameof(DisplayedClientsList)));
			}
		}

		Department _selectedDepartment;
		public Department SelectedDepartment
		{
			get => _selectedDepartment;
			set
			{
				_selectedDepartment = value;
				DisplayedClientsList = new ObservableCollection<ClientVM>
					(ClientsList.Where(clientVM => clientVM.DepartmentID == _selectedDepartment.Id));
				DisplayedClientsList.CollectionChanged += ClientsList_CollectionChanged;
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;


		private void ClientsList_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			foreach (ClientVM item in e.NewItems)
			{
				item.DepartmentID = SelectedDepartment.Id;
				ClientsList.Add(item);
			}
		}

		public ObservableCollection<ClientVM> ClientsList { get; set; }
		public bool IsReadOnly { get; set; }
		public bool IsCanAddNewClient { get; set; }
		public IClientRepository ClientsRepository { get; set; }

		internal void Save()
		{
			var clientsList = ClientsList.Select(ConvertToClient).ToList();
			ClientsRepository.Save(clientsList);
		}

		public Client ConvertToClient(ClientVM clientVM) => new Client()
		{
			DepartmentId = clientVM.DepartmentID,
			LastName = clientVM.LastName,
			FirstName = clientVM.FirstName,
			MiddleName = clientVM.MiddleName,
			PhoneNumber = clientVM.PhoneNumber,
			Passport = clientVM.Passport,
			ChangingTime = clientVM.ChangingTime,
			ChangedData = clientVM.ChangedData,
			ChangingType = clientVM.ChangingType,
			LastChanger = clientVM.LastChanger
		};
	}
}
