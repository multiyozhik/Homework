using System.Windows;

namespace Connections
{
	/// <summary>
	/// Логика взаимодействия для ClientData.xaml
	/// </summary>
	public partial class ClientDataWindow : Window
	{
		public ClientDataWindow()
		{
			InitializeComponent();

		}
		private ClientData? newClientData; //инкапсуляция (изменить извне я не могу, только ч/з формочку)
		private void ClientDataDone_Click(object sender, RoutedEventArgs e)
		{
			newClientData = new ClientData(
				FamilyNameTexBox.Text,
				FirstNameTextBox.Text,
				MiddleNameTextBox.Text,
				PhoneNumberTextBox.Text,
				EmailTextBox.Text
				);
			Close();
		}
		internal ClientData? GetNewClientData() => newClientData; //публичный метод для получения новых данных клиента извне
	}
}

