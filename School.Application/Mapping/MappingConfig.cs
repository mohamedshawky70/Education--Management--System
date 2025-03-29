using Mapster;
using School.Application.DTOs.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace School.Application.Mapping
{
	public class MappingConfig : IRegister
	{
		public void Register(TypeAdapterConfig config)
		{
			config.NewConfig<Student, StudentResponse>()
				.Map(des => des.DepartmentName, src => src.Department.Name);
		}
	}
}
