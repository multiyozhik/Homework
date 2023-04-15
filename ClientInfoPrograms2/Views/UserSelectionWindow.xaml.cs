using ClientsInfoProgram.ViewModels;
using System.Windows;

namespace ClientsInfoProgram.Views	
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class UserSelectionWindow : Window
	{
		public UserSelectionWindow()
		{
			InitializeComponent();
			DataContext = new UserSelectionVM();
		}		
	}
}
