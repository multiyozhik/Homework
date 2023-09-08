namespace _19_Notebook.Models
{
	public record class Contact(
		Guid Id, 
		string FamilyName, 
		string Name, 
		string MiddleName, 
		string PhoneNumber, 
		string Adress)
	{

		public string Description => $"{FamilyName}: {PhoneNumber}";

	}
}
