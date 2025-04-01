namespace School.Infrastructure.Implementation.Services
{
	public class StudentService(IUnitOfWork unitOfWork) : IStudentService
	{
		private readonly IUnitOfWork _unitOfWork = unitOfWork;

		public async Task<OneOf<StudentResponse, Errors>> GetByIdAsync(int id, CancellationToken cancellationToken = default)
		{
			var student = await _unitOfWork.Student.FindByInclude(x => x.Id == id, cancellationToken, ["Department"]);
			if (student is null)
				return StudentErrors.NotFound;

			var Response = student.Adapt<StudentResponse>();
			return Response;
		}
		public async Task<IEnumerable<StudentResponse>> GetAllAsync(CancellationToken cancellationToken = default)
		{
			var students = await _unitOfWork.Student.FindAllInclude(cancellationToken, ["Department"]);
			var response = students.Adapt<IEnumerable<StudentResponse>>();
			return response;
		}
		public async Task<OneOf<StudentResponse, Errors>> CreateAsync(StudentRequest request, CancellationToken cancellationToken = default)
		{
			var existedStudent = await _unitOfWork.Student.FindByInclude(x => x.Name == request.Name
				&& x.Address == request.Address && x.Phone == request.Phone, cancellationToken);
			if (existedStudent is not null)
				return StudentErrors.Duplicate;

			var student = request.Adapt<Student>();
			await _unitOfWork.Student.CreateAsync(student, cancellationToken);
			var studentWithDepartment = await _unitOfWork.Student.FindByInclude(x => x.Id == student.Id, cancellationToken, new[] { "Department" });
			var response = studentWithDepartment.Adapt<StudentResponse>();
			return response;
		}
		public async Task<OneOf<StudentResponse, Errors>> UpdateAsync(int id, StudentRequest request, CancellationToken cancellationToken = default)
		{
			var student = await _unitOfWork.Student.FindByInclude(x => x.Id == id, cancellationToken, ["Department"]);
			if (student is null)
				return StudentErrors.NotFound;

			var existedStudent = await _unitOfWork.Student.FindByInclude(x => x.Name == request.Name
				&& x.Address == request.Address && x.Phone == request.Phone && x.Id != id!, cancellationToken);
			if (existedStudent is not null)
				return StudentErrors.Duplicate;
			//Map data in object to [data] in object
			var newStudent = request.Adapt(student);
			await _unitOfWork.Student.UpdateAsync(newStudent, cancellationToken);

			var studentWithDepartment = await _unitOfWork.Student.FindByInclude(x => x.Id == student.Id, cancellationToken, ["Department"]);
			var response = studentWithDepartment.Adapt<StudentResponse>();
			return response;
		}
		public async Task<OneOf<Errors?>> DeleteAsync(int id, CancellationToken cancellationToken = default)
		{
			var student = await _unitOfWork.Student.GetByIdAsync(id, cancellationToken);
			if (student is null)
				return StudentErrors.NotFound;

			await _unitOfWork.Student.DeleteAsync(student, cancellationToken);
			return null;
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
				return SubjectErrors.Dublicate;

			var studentSubject = request.Adapt<StudentSubject>();
			await _unitOfWork.StudentSubject.CreateAsync(studentSubject, cancellationToken);
			return null;
		}
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
			await _unitOfWork.StudentSubject.DeleteAsync(studentSubject);
			return null;
		}
	}
}
