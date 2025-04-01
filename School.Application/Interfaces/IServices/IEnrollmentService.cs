using OneOf;
using School.Application.DTOs.Student;
using School.Application.DTOs.StudentSubject;
using School.Application.ErrorHandler;

namespace School.Application.Interfaces.IServices
{
	public interface IEnrollmentService
	{
		Task<OneOf<Errors?>> CreateStudentSubjectAsync(StudentSubjectRequest request, CancellationToken cancellationToken = default);
		Task<OneOf<IEnumerable<string>, Errors>> GetStudentSubjectsAsync(int id, CancellationToken cancellationToken = default);
		Task<OneOf<Errors?>> DropStudentFromSubjectAsync(StudentSubjectRequest request, CancellationToken cancellationToken = default);
		Task<IEnumerable<StudentSubjectResponse>> GetStudentsSubjectsAsync(CancellationToken cancellationToken = default);

	}
}
