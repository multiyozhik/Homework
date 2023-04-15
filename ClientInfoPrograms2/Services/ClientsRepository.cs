using ClientsInfoProgram.Models;
using System.Collections.Generic;
using System.Linq;

namespace ClientsInfoProgram.Services
{
	class ClientsRepository : IClientRepository
	{			
		List<Client> ClientsList { get; set; } = new()
		{
			new Client
			{
				DepartmentId = 1,
				LastName = "Иванов",
				FirstName = "Иван",
				MiddleName = "Иванович",
				PhoneNumber = "224-88-99",
				Passport = "2005 456789"
			},
			new Client
			{
				DepartmentId = 2,
				LastName = "Петров",
				FirstName = "Петр",
				MiddleName = "Петрович",
				PhoneNumber = "225-88-00",
				Passport = "2008 458888"
			},
			new Client
			{
				DepartmentId = 3,
				LastName = "Васильев",
				FirstName = "Василий",
				MiddleName = "Васильевич",
				PhoneNumber = "228-89-05",
				Passport = "2007 456994"
			}
		};


		void IClientRepository.Save(ICollection<Client> clients) => ClientsList = clients.ToList();	

		ICollection<Client> IClientRepository.GetClients() => ClientsList;		
	}
}
