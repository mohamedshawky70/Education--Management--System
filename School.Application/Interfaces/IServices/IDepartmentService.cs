using OneOf;
using School.Application.DTOs.Department;
using School.Application.ErrorHandler;

namespace School.Application.Interfaces.IServices
{
	public interface IDepartmentService
	{
		Task<OneOf<DepartmentResponse, Errors>> GetByIdAsync(int id, CancellationToken cancellationToken = default);
		Task<IEnumerable<DepartmentResponse>> GetAllAsync(CancellationToken cancellationToken = default);
		Task<OneOf<DepartmentResponse, Errors>> CreateAsync(DepartmentRequest request, CancellationToken cancellationToken = default);
		Task<OneOf<DepartmentResponse, Errors>> UpdateAsync(int id, DepartmentRequest request, CancellationToken cancellationToken = default);
		Task<OneOf<Errors?>> DeleteAsync(int id, CancellationToken cancellationToken = default);

	}
}
