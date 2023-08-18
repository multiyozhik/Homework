using System;
using System.Collections.Generic;
using System.Windows;

namespace _18_Animals.Views
{
	public partial class AnimalCardWindow : Window, IAnimalCardView
	{
		public string? AnimalClass => AnimalClassComboBox.SelectedItem?.ToString();

		public string? NickName => NickNameTextBox.Text;

		public string? Name => AnimalNameComboBox.SelectedItem?.ToString();

		string? gender;		
		public string? Gender
		{
			get => gender = GenderComboBox.SelectedItem?.ToString();
			set => gender = value;
		}

		public DateTime? BirthDay => BithDayCalendar.SelectedDate;

		public AnimalCardWindow()
		{
			InitializeComponent();
			AnimalClassComboBox.ItemsSource = new List<string>() { "пресмыкающееся", "земноводное", "млекопитающее", "птица", "рыба" };
			GenderComboBox.ItemsSource = new List<string> { "самец", "самка", "не определен" };
			AnimalClassComboBox.SelectionChanged +=
				(s, e) => AnimalNameComboBox.ItemsSource = AnimalClassComboBox.SelectedItem.ToString() switch
				{
					"пресмыкающееся" => new List<string>() { "ящерица", "змея", "крокодил" },
					"земноводное" => new List<string>() { "лягушка", "тритон" },
					"млекопитающее" => new List<string>() { "волк", "медведь", "лисица" },
					"птица" => new List<string>() { "попугай", "сова", "утка" },
					"рыба" => new List<string>() { "цихлазома", "скалярия" },
					_ => new List<string>() { }
				};
			BithDayCalendar.SelectedDate = DateTime.Today;
			SaveAnimalCardButton.Click += (s, e) => SaveAnimalCard();
		}

		public void SaveAnimalCard()
		{			
				DialogResult = true;
				Close();			
		}
	}
}
