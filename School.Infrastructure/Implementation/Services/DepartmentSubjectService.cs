using School.Application.DTOs.DepartmentSubject;

namespace School.Infrastructure.Implementation.Services
{
	public class DepartmentSubjectService(IUnitOfWork unitOfWork) : IDepartmentSubjectService
	{
		private readonly IUnitOfWork _unitOfWork = unitOfWork;
		public async Task<OneOf<IEnumerable<string>, Errors>> GetDepartmentSubjectsAsync(int id, CancellationToken cancellationToken = default)
		{
			var department = await _unitOfWork.Department.GetByIdAsync(id, cancellationToken);
			if (department is null)
				return DepartmentErrors.NotFound;

			var departmentSubject = await _unitOfWork.DepartmentSubject.FindAllByInclude(x => x.DepartmentId == id);
			List<string> NameSubjects = [];
			foreach (var subId in departmentSubject)
			{
				var subject = await _unitOfWork.Subject.GetByIdAsync(subId.SubjectId, cancellationToken);
				NameSubjects.Add(subject!.Name);
			}
			return NameSubjects;
		}
		public async Task<IEnumerable<DepartmentSubjectResponse>> GetDepartmentsSubjectsAsync(CancellationToken cancellationToken = default)
		{
			var departmentsSubjects = await _unitOfWork.DepartmentSubject.FindAllInclude(cancellationToken, ["Department", "Subject"]);
			var response = departmentsSubjects.Adapt<IEnumerable<DepartmentSubjectResponse>>();
			return response;
		}

		public async Task<OneOf<Errors?>> CreateDepartmentSubjectAsync(DepartmentSubjectRequest request, CancellationToken cancellationToken = default)
		{
			var student = await _unitOfWork.Department.GetByIdAsync(request.DepartmentId, cancellationToken);
			if (student is null)
				return DepartmentErrors.NotFound;
			var subject = await _unitOfWork.Subject.GetByIdAsync(request.SubjectId, cancellationToken);
			if (subject is null)
				return SubjectErrors.NotFound;

			var ExistedDepartmentSubject = await _unitOfWork.DepartmentSubject.FindByInclude(x => x.DepartmentId == request.DepartmentId && x.SubjectId == request.SubjectId);
			if (ExistedDepartmentSubject is not null)
				return DepartmentSubjectErrors.Dublicate;

			var departmentSubject = request.Adapt<DepartmentSubject>();
			await _unitOfWork.DepartmentSubject.CreateAsync(departmentSubject, cancellationToken);
			return null;
		}
		public async Task<OneOf<Errors?>> DropDepartmentFromSubjectAsync(DepartmentSubjectRequest request, CancellationToken cancellationToken = default)
		{
			var department = await _unitOfWork.Department.GetByIdAsync(request.DepartmentId, cancellationToken);
			if (department is null)
				return DepartmentErrors.NotFound;
			var subject = await _unitOfWork.Subject.GetByIdAsync(request.SubjectId, cancellationToken);
			if (subject is null)
				return SubjectErrors.NotFound;

			var departmentSubject = await _unitOfWork.DepartmentSubject.FindByInclude(x => x.DepartmentId == request.DepartmentId && x.SubjectId == request.SubjectId);
			if (departmentSubject is null)
				return DepartmentErrors.DepartmentHasNotThisSubjectOrReverse;
			await _unitOfWork.DepartmentSubject.DeleteAsync(departmentSubject, cancellationToken);
			return null;
		}

	}
}
