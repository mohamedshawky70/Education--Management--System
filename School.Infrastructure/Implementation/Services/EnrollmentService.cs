using School.Application.DTOs.StudentSubject;

namespace School.Infrastructure.Implementation.Services
{
	public class EnrollmentService(IUnitOfWork unitOfWork) : IEnrollmentService
	{
		private readonly IUnitOfWork _unitOfWork = unitOfWork;
		public async Task<OneOf<IEnumerable<string>, Errors>> GetStudentSubjectsAsync(int id, CancellationToken cancellationToken = default)
		{
			var student = await _unitOfWork.Student.GetByIdAsync(id, cancellationToken);
			if (student is null)
				return StudentErrors.NotFound;

			var studentSubject = await _unitOfWork.StudentSubject.FindAllByInclude(x => x.StudentId == id);
			List<string> NameSubjects = [];
			foreach (var subId in studentSubject)
			{
				var subject = await _unitOfWork.Subject.GetByIdAsync(subId.SubjectId, cancellationToken);
				NameSubjects.Add(subject!.Name);
			}
			return NameSubjects;
		}
		public async Task<OneOf<Errors?>> CreateStudentSubjectAsync(StudentSubjectRequest request, CancellationToken cancellationToken = default)
		{
			var student = await _unitOfWork.Student.GetByIdAsync(request.StudentId, cancellationToken);
			if (student is null)
				return StudentErrors.NotFound;
			var subject = await _unitOfWork.Subject.GetByIdAsync(request.SubjectId, cancellationToken);
			if (subject is null)
				return SubjectErrors.NotFound;
			var ExistedStudentSubject = await _unitOfWork.StudentSubject.FindByInclude(x => x.StudentId == request.StudentId && x.SubjectId == request.SubjectId);
			if (ExistedStudentSubject is not null)
				return DepartmentSubjectErrors.Dublicate;

			var studentSubject = request.Adapt<StudentSubject>();
			await _unitOfWork.StudentSubject.CreateAsync(studentSubject, cancellationToken);
			return null;
		}
		public async Task<IEnumerable<StudentSubjectResponse>> GetStudentsSubjectsAsync(CancellationToken cancellationToken = default)
		{
			var studentsSubjects = await _unitOfWork.StudentSubject.FindAllInclude(cancellationToken, ["Student", "Subject"]);
			var response = studentsSubjects.Adapt<IEnumerable<StudentSubjectResponse>>();
			return response;
		}
		public async Task<OneOf<Errors?>> DropStudentFromSubjectAsync(StudentSubjectRequest request, CancellationToken cancellationToken = default)
		{
			var student = await _unitOfWork.Student.GetByIdAsync(request.StudentId, cancellationToken);
			if (student is null)
				return StudentErrors.NotFound;
			var subject = await _unitOfWork.Subject.GetByIdAsync(request.SubjectId, cancellationToken);
			if (subject is null)
				return SubjectErrors.NotFound;

			var studentSubject = await _unitOfWork.StudentSubject.FindByInclude(x => x.StudentId == request.StudentId && x.SubjectId == request.SubjectId);
			if (studentSubject is null)
				return StudentErrors.StudentHasNotThisSubjectOrReverse;
			await _unitOfWork.StudentSubject.DeleteAsync(studentSubject, cancellationToken);
			return null;
		}

	}
}
