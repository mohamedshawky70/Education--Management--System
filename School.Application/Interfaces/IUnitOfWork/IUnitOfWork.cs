using School.Application.Interfaces.BaseRepo;

namespace School.Application.Interfaces.IUnitOfWork
{
	public interface IUnitOfWork
	{
		public IBaseRepo<Student> Student { get; }
		public IBaseRepo<Department> Department { get; }
		public IBaseRepo<StudentSubject> StudentSubject { get; }
		public IBaseRepo<Subject> Subject { get; }
		public IBaseRepo<Assignment> Assignment { get; }
		public IBaseRepo<DepartmentSubject> DepartmentSubject { get; }
		public IBaseRepo<Teacher> Teacher { get; }
		int Complete();
	}
}
