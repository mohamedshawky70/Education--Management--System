using Microsoft.EntityFrameworkCore;
using School.Application.Interfaces.BaseRepo;
using School.Application.Interfaces.IUnitOfWork;
using School.Infrastructure.Data;
using School.Infrastructure.Implementation.BaseRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Infrastructure.Implementation.UnitOfWork
{
	public class UnitOfWork(ApplicationDbContext _dbContext) : IUnitOfWork
	{
		private readonly ApplicationDbContext dbContext = _dbContext;

		public IBaseRepo<Student> Student => new BaseRepo<Student>(_dbContext);

		public IBaseRepo<Department> Department => new BaseRepo<Department>(_dbContext);

		public IBaseRepo<StudentSubject> StudentSubject => new BaseRepo<StudentSubject>(_dbContext);
		public IBaseRepo<Subject> Subject => new BaseRepo<Subject>(_dbContext);

		public int Complete()
		{
			return _dbContext.SaveChanges();
		}
	}
}
