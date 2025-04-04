using School.Application.DTOs.Department;
using School.Application.DTOs.Teacher;
using School.Application.ErrorHandler;

namespace School.Infrastructure.Implementation.Services
{
	public class TeacherService(IUnitOfWork unitOfWork) : ITeacherService
	{
		private readonly IUnitOfWork _unitOfWork = unitOfWork;
		public async Task<OneOf<TeacherResponse, Errors>> GetByIdAsync(int id, CancellationToken cancellationToken = default)
		{			
			var teacher = await _unitOfWork.Teacher.FindByInclude(x=>x.Id==id ,cancellationToken,["Subject"]);
			if (teacher is null)
				return TeacherErrors.NotFound;

			var Response = teacher.Adapt<TeacherResponse>();
			return Response;
		}
		public async Task<IEnumerable<TeacherResponse>> GetAllAsync(CancellationToken cancellationToken = default)
		{
			var teachers = await _unitOfWork.Teacher.FindAllInclude( cancellationToken, ["Subject"]);
			var response = teachers.Adapt<IEnumerable<TeacherResponse>>();
			return response;
		}
		public async Task<OneOf<TeacherResponse, Errors>> CreateAsync(TeacherRequest request, CancellationToken cancellationToken = default)
		{
			var existedTeacher = await _unitOfWork.Teacher.FindByInclude(x => x.Email == request.Email);
			if (existedTeacher is not null)
				return TeacherErrors.Duplicate;

			var teacher = request.Adapt<Teacher>();
			await _unitOfWork.Teacher.CreateAsync(teacher, cancellationToken);
			var response = teacher.Adapt<TeacherResponse>();
			return response;
		}
		public async Task<OneOf<TeacherResponse, Errors>> UpdateAsync(int id, TeacherRequest request, CancellationToken cancellationToken = default)
		{
			var teacher = await _unitOfWork.Teacher.GetByIdAsync(id, cancellationToken);
			if (teacher is null)
				return TeacherErrors.NotFound;
			var teacherExisted = await _unitOfWork.Teacher.FindByInclude(x => x.Email == request.Email && x.Id != id, cancellationToken);
			if (teacherExisted is not null)
				return TeacherErrors.Duplicate;
			//Map data in object to [data] in object
			var newTeacher = request.Adapt(teacher);
			await _unitOfWork.Teacher.UpdateAsync(newTeacher, cancellationToken);

			var response = newTeacher.Adapt<TeacherResponse>();
			return response;
		}
		public async Task<OneOf<Errors?>> DeleteAsync(int id, CancellationToken cancellationToken = default)
		{
			var teacher = await _unitOfWork.Teacher.GetByIdAsync(id, cancellationToken);
			if (teacher is null)
				return TeacherErrors.NotFound;
			if (teacher.SubjectId!=null)
				return TeacherErrors.AssignedToSubject;

			await _unitOfWork.Teacher.DeleteAsync(teacher, cancellationToken);
			return null;
		}
	}
}
