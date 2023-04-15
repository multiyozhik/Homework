using ClientsInfoProgram.Models;
using System.Collections.Generic;


namespace ClientsInfoProgram.Services
{
	interface IClientRepository
	{
		ICollection<Client> GetClients();
		void Save(ICollection<Client> clients);
	}
}
