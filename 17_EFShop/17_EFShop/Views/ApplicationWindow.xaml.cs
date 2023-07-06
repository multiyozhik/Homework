using _17_EFShop.ViewModels;
using System.Windows;

namespace _17_EFShop.Views
{
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class ApplicationWindow : Window
    {
        public ApplicationWindow()
        {
            InitializeComponent();
            DataContext = new ApplicationVM();
        }
	}
}
