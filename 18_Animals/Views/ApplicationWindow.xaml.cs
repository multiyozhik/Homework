using _18_Animals.Models;
using _18_Animals.Presenters;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Data;

namespace _18_Animals.Views
{
	public partial class ApplicationWindow : Window, IApplicationView
	{
		readonly Presenter presenter;

		public string? ExportFormat => ExportFormatComboBox.SelectedItem.ToString();

		public IAnimal? SelectedAnimal => AnimalsListView.SelectedItem as IAnimal;

		public ApplicationWindow()
		{
			InitializeComponent();
			ExportFormatComboBox.ItemsSource = new List<string>() { "txt", "csv" };

			presenter = new Presenter(this);

			AddButton.Click += (s, e) => presenter.Add();
			ChangeButton.Click += (s, e) => presenter.Change();
			RemoveButton.Click += (s, e) => presenter.Remove();
			ExportButton.Click += (s, e) => presenter.Export();
		}

		public void AddAnimal(IAnimal newAnimal)
		{
			AnimalsListView.Items.Add(newAnimal);
		}

		public void Refresh()
		{
			//https://stackoverflow.com/questions/4488463/how-i-can-refresh-listview-in-wpf
			var view = CollectionViewSource.GetDefaultView(AnimalsListView.Items);
			view.Refresh(); //обновляет представление для отображения изменений
		}

		public void RemoveAnimal(IAnimal selectedAnimal)
		{
			AnimalsListView.Items.Remove(selectedAnimal);
		}
	}
}
