using School.Application.DTOs.Subject;

namespace School.Infrastructure.Implementation.Services
{
	public class SubjectService(IUnitOfWork unitOfWork) : ISubjectService
	{
		private readonly IUnitOfWork _unitOfWork = unitOfWork;

		public async Task<OneOf<SubjectResponse, Errors>> GetByIdAsync(int id, CancellationToken cancellationToken = default)
		{
			var subject = await _unitOfWork.Subject.FindByInclude(x => x.Id == id, cancellationToken, ["Teacher"]);
			if (subject is null)
				return StudentErrors.NotFound;

			var Response = subject.Adapt<SubjectResponse>();
			return Response;
		}
		public async Task<IEnumerable<SubjectResponse>> GetAllAsync(CancellationToken cancellationToken = default)
		{
			var subjects = await _unitOfWork.Subject.FindAllInclude(cancellationToken, ["Teacher"]);
			var response = subjects.Adapt<IEnumerable<SubjectResponse>>();
			return response;
		}
		public async Task<OneOf<SubjectResponse, Errors>> CreateAsync(SubjectRequest request, CancellationToken cancellationToken = default)
		{
			var existedSubject = await _unitOfWork.Subject.FindByInclude(x => x.Name == request.Name
				&& x.Period == request.Period, cancellationToken);
			if (existedSubject is not null)
				return SubjectErrors.Dublicate;
			var teacher = await _unitOfWork.Teacher.GetByIdAsync(request.TeacherId, cancellationToken);
			if (teacher is null)
				return TeacherErrors.NotFound;

			var subject = request.Adapt<Subject>();
			await _unitOfWork.Subject.CreateAsync(subject, cancellationToken);
			var response = subject.Adapt<SubjectResponse>();
			return response;
		}
		public async Task<OneOf<SubjectResponse, Errors>> UpdateAsync(int id, SubjectRequest request, CancellationToken cancellationToken = default)
		{
			var subject = await _unitOfWork.Subject.GetByIdAsync(id, cancellationToken);
			if (subject is null)
				return SubjectErrors.NotFound;

			var existedSubject = await _unitOfWork.Subject.FindByInclude(x => x.Name == request.Name
				&& x.Period == request.Period && x.Id != id, cancellationToken);
			if (existedSubject is not null)
				return SubjectErrors.Dublicate;
			//Map data in object to [data] in object
			var newSubject = request.Adapt(subject);
			await _unitOfWork.Subject.UpdateAsync(newSubject, cancellationToken);
			var response = newSubject.Adapt<SubjectResponse>();
			return response;
		}
		public async Task<OneOf<Errors?>> DeleteAsync(int id, CancellationToken cancellationToken = default)
		{
			var subject = await _unitOfWork.Subject.GetByIdAsync(id, cancellationToken);
			if (subject is null)
				return SubjectErrors.NotFound;
			var IsEnrolledSubject = await _unitOfWork.StudentSubject.GetById(id, cancellationToken);
			if (IsEnrolledSubject is null)
				return SubjectErrors.Enrolled;

			await _unitOfWork.Subject.DeleteAsync(subject, cancellationToken);
			return null;
		}
	}
}
