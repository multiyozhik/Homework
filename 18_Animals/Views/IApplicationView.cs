using _18_Animals.Models;

namespace _18_Animals.Views
{
	public interface IApplicationView
    {          
        string? ExportFormat { get;}
		
		IAnimal? SelectedAnimal { get; }

		void AddAnimal(IAnimal newAnimal);
		
		void Refresh(); 

		void RemoveAnimal(IAnimal selectedAnimal);
	}
}
