using System.Windows;

namespace _17_EFShop
{
	/// <summary>
	/// Логика взаимодействия для AddClientWindow.xaml
	/// </summary>
	public partial class ClientDataWindow : Window
	{
		public ClientDataWindow()
		{
			InitializeComponent();
		}

		private void DoneButton_Click(object sender, RoutedEventArgs e)
		{			
			this.DialogResult = true;
			Close();
		}
		
	}
}
