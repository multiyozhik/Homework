using _18_Animals.Models;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _18_Animals
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		Repository animalRepositoty = new Repository(new List<IAnimal>());


		private void CreateButton_Click(object sender, RoutedEventArgs e)
		{
			var factory = new Factory();
			var selectedAnimalClass = AnimalClass.SelectedItem.ToString();
			if (selectedAnimalClass is null) return;
			var newAnimal = factory.Create(selectedAnimalClass);
			animalRepositoty.Add(newAnimal);
			MessageBox.Show($"Создано животное - {newAnimal.Name}");
		}

		private void SaveButton_Click(object sender, RoutedEventArgs e)
		{
			animalRepositoty.Save(new TxtSaver());
		}
	}
}
