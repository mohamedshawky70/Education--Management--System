using OneOf;
using School.Application.DTOs.DepartmentSubject;
using School.Application.ErrorHandler;

namespace School.Application.Interfaces.IServices
{
	public interface IDepartmentSubjectService
	{
		Task<OneOf<IEnumerable<string>, Errors>> GetDepartmentSubjectsAsync(int id, CancellationToken cancellationToken = default);
		Task<IEnumerable<DepartmentSubjectResponse>> GetDepartmentsSubjectsAsync(CancellationToken cancellationToken = default);
		Task<OneOf<Errors?>> CreateDepartmentSubjectAsync(DepartmentSubjectRequest request, CancellationToken cancellationToken = default);
		Task<OneOf<Errors?>> DropDepartmentFromSubjectAsync(DepartmentSubjectRequest request, CancellationToken cancellationToken = default);
	}
}
