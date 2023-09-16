namespace _20_NotebookDb.Models
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
