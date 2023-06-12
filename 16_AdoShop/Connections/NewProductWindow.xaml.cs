using System.Windows;

namespace Connections
{
	/// <summary>
	/// Логика взаимодействия для NewShoppingWindow.xaml
	/// </summary>
	public partial class NewProductWindow : Window
	{
		public NewProductWindow()
		{
			InitializeComponent();
		}
		private ProductData? newProductData;
		private void ProductDataDone_Click(object sender, RoutedEventArgs e)
		{
			newProductData = new ProductData(int.Parse(ProductCode.Text), ProductName.Text);
			Close();
		}
		internal ProductData? GetNewProductData() => newProductData;
	}
}
