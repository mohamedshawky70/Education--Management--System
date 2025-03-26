using School.Application.Interfaces.BaseRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Application.Interfaces.IUnitOfWork
{
    public interface IUnitOfWork
    {
		public IBaseRepo<Student> Student { get;}
		public IBaseRepo<Department> Department { get;}
		int Complete();
	}
}
