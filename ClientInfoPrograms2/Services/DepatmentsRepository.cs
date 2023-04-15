using System.Collections.Generic;
using ClientsInfoProgram.Models;

namespace ClientsInfoProgram.Services
{
	class DepatmentsRepository : IDepartmentRepository
	{
		List<Department> DepartmentsList { get; set; } = new List<Department>
		{
			new Department(1,"отдел продаж"),
			new Department(2,"отдел закупок"),
			new Department(3,"отдел рекламы")
		};
		ICollection<Department> IDepartmentRepository.GetDepartmets() => DepartmentsList;		
	}
}
