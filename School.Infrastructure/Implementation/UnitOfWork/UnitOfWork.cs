using School.Application.Interfaces.BaseRepo;
using School.Infrastructure.Data;
using School.Infrastructure.Implementation.BaseRepo;

namespace School.Infrastructure.Implementation.UnitOfWork
{
	public class UnitOfWork(ApplicationDbContext _dbContext) : IUnitOfWork
	{
		private readonly ApplicationDbContext dbContext = _dbContext;

		public IBaseRepo<Student> Student => new BaseRepo<Student>(_dbContext);

		public IBaseRepo<Department> Department => new BaseRepo<Department>(_dbContext);

		public IBaseRepo<StudentSubject> StudentSubject => new BaseRepo<StudentSubject>(_dbContext);
		public IBaseRepo<Subject> Subject => new BaseRepo<Subject>(_dbContext);

		public IBaseRepo<Assignment> Assignment => new BaseRepo<Assignment>(_dbContext);

		public IBaseRepo<DepartmentSubject> DepartmentSubject => new BaseRepo<DepartmentSubject>(_dbContext);

		public IBaseRepo<Teacher> Teacher => new BaseRepo<Teacher>(_dbContext);

		public int Complete()
		{
			return _dbContext.SaveChanges();
		}
	}
}
