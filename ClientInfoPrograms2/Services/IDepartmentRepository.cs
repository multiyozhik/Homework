using ClientsInfoProgram.Models;
using System.Collections.Generic;


namespace ClientsInfoProgram.Services
{
	interface IDepartmentRepository
	{
		ICollection<Department> GetDepartmets();		
	}
}
