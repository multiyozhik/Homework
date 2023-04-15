using ClientsInfoProgram.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;

namespace ClientsInfoProgram.Views
{
	/// <summary>
	/// Логика взаимодействия для Window1.xaml
	/// </summary>
	public partial class ClientsInfoWindow : Window
	{
		public ClientsInfoWindow()
		{
			InitializeComponent();
			this.AddHandler(Validation.ErrorEvent, new RoutedEventHandler(OnErrorEvent));
		}
		       
        private void OnErrorEvent(object sender, RoutedEventArgs e)
        {
			bool IsErrors = false;
			var clientInfoVM = (ClientsInfoVM)DataContext;
			foreach (ClientVM clientVM in clientInfoVM.ClientsList)
			{
				if (!String.IsNullOrEmpty(clientVM.Error))
				{
					IsErrors = true;
					break;
				}
			}
			SaveButton.IsEnabled = !IsErrors;
		}
        private void SaveButton_Click(object sender, RoutedEventArgs e)
		{
			var clientInfoVM = (ClientsInfoVM)DataContext;
			clientInfoVM.Save();
			Close();
		}
	}
}
