namespace _18_Animals.Views
{
	internal interface IAnimalChangingsView
	{		
		public string? Gender { get; set; }

		public void SaveChangings();
	}
}
