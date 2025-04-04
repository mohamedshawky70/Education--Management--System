using OneOf;
using School.Application.DTOs.Student;
using School.Application.ErrorHandler;

namespace School.Application.Interfaces.IServices
{
	public interface IStudentService
	{
		Task<OneOf<StudentResponseV1, Errors>> GetByIdAsyncV1(int id, CancellationToken cancellationToken = default);
		Task<OneOf<StudentResponseV2, Errors>> GetByIdAsyncV2(int id, CancellationToken cancellationToken = default);
		Task<IEnumerable<StudentResponseV2>> GetAllAsync(CancellationToken cancellationToken = default);
		Task<OneOf<StudentResponseV2, Errors>> CreateAsync(StudentRequest request, CancellationToken cancellationToken = default);
		Task<OneOf<StudentResponseV2, Errors>> UpdateAsync(int id, StudentRequest request, CancellationToken cancellationToken = default);
		Task<OneOf<Errors?>> DeleteAsync(int id, CancellationToken cancellationToken = default);

	}
}
