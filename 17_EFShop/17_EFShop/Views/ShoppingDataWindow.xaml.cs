using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace _17_EFShop.Views
{
    /// <summary>
    /// Логика взаимодействия для ProductDataWindow.xaml
    /// </summary>
    public partial class ShoppingDataWindow : Window
    {
        public ShoppingDataWindow()
        {
            InitializeComponent();
        }

		private void DoShoppingButton_Click(object sender, RoutedEventArgs e)
		{
            DialogResult = true;
            Close();
		}
	}
}
