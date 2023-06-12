namespace Connections
{
	class ClientData
	{		
		public string FamilyName { get;}
		public string FirstName { get;}
		public string MiddleName { get;}
		public string? PhoneNumber { get;}
		public string Email { get; }	

		public ClientData(
			string familyName, string firstName, string middleName, string phoneNumber, string email)
		{			
			FamilyName = familyName;
			FirstName = firstName;
			MiddleName = middleName;
			PhoneNumber = phoneNumber;
			Email = email;
		}
	}
}
