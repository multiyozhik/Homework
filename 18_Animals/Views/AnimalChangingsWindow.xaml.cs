using System.Collections.Generic;
using System.Windows;

namespace _18_Animals.Views
{
	/// <summary>
	/// Логика взаимодействия для AnimalChangingsWindow.xaml
	/// </summary>
	public partial class AnimalChangingsWindow : Window, IAnimalChangingsView
	{		
		string? gender;
		public string? Gender
		{
			get => gender = GenderComboBox.SelectedItem.ToString();
			set => gender = value;
		}

		public AnimalChangingsWindow(string animalDescription)
		{
			InitializeComponent();
			AnimalDiscription.Text = animalDescription;
			GenderComboBox.ItemsSource = new List<string>() { "самец", "самка" };
			SaveChangingsButton.Click += (s,e) => SaveChangings();
		}

		public void SaveChangings()
		{
			DialogResult = true;
			Close();
		}
	}
}
