using OneOf;
using School.Application.DTOs.Subject;
using School.Application.ErrorHandler;

namespace School.Application.Interfaces.IServices
{
	public interface ISubjectService
	{
		Task<OneOf<SubjectResponse, Errors>> GetByIdAsync(int id, CancellationToken cancellationToken = default);
		Task<IEnumerable<SubjectResponse>> GetAllAsync(CancellationToken cancellationToken = default);
		Task<OneOf<SubjectResponse, Errors>> CreateAsync(SubjectRequest request, CancellationToken cancellationToken = default);
		Task<OneOf<SubjectResponse, Errors>> UpdateAsync(int id, SubjectRequest request, CancellationToken cancellationToken = default);
		Task<OneOf<Errors?>> DeleteAsync(int id, CancellationToken cancellationToken = default);

	}
}
