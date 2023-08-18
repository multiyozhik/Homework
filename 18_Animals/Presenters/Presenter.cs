using _18_Animals.Models;
using _18_Animals.Views;

namespace _18_Animals.Presenters
{
	class Presenter
	{
		readonly IApplicationView applicationView;

		readonly Model model;

		public Presenter(IApplicationView applicationView)
		{
			this.applicationView = applicationView;
			model = new Model();
		}

		internal void Add()
		{
			var animalCardWindow = new AnimalCardWindow();
			var result = animalCardWindow.ShowDialog();
			if (result is not true) return;

			var animalData = new AnimalData(
				animalCardWindow.AnimalClass,
				animalCardWindow.AnimalSpecies,
				animalCardWindow.NickName,
				animalCardWindow.Gender,
				animalCardWindow.BirthDay);

			var newAnimal = model.Add(animalData);
			applicationView.AddAnimal(newAnimal);
		}

		internal void Change()
		{
			var selectedAnimal = applicationView.SelectedAnimal;
			if (selectedAnimal is null) return;
			var animalDescription = $"Id {selectedAnimal.Id} - {selectedAnimal.AnimalSpecies} {selectedAnimal.NickName}";
			var animalChangingsWindow = new AnimalChangingsWindow(animalDescription);
			var result = animalChangingsWindow.ShowDialog();
			if (result is not true) return;
			var newGender = animalChangingsWindow.Gender;
			model.Change(selectedAnimal, newGender);
			applicationView.Refresh(); //обновить представление
		}
		internal void Remove()
		{
			if (applicationView.SelectedAnimal is null) return;
			applicationView.RemoveAnimal(applicationView.SelectedAnimal);
		}

		internal void Export()
		{
			if (applicationView.ExportFormat is null) return;
			model.Export(applicationView.ExportFormat);
		}
	}
}
