using Microsoft.Extensions.Caching.Hybrid;
using System.Collections.Generic;

namespace School.Infrastructure.Implementation.Services
{
	public class StudentService(IUnitOfWork unitOfWork,HybridCache hybridCache) : IStudentService
	{
		private const string cashPrefix = "allStudent";
		private readonly IUnitOfWork _unitOfWork = unitOfWork;
		private readonly HybridCache _hybridCache = hybridCache;

		public async Task<OneOf<StudentResponseV1, Errors>> GetByIdAsyncV1(int id, CancellationToken cancellationToken = default)
		{
			var student = await _unitOfWork.Student.FindByInclude(x => x.Id == id, cancellationToken, ["Department"]);
			if (student is null)
				return StudentErrors.NotFound;

			var Response = student.Adapt<StudentResponseV1>();
			return Response;
		}
		public async Task<OneOf<StudentResponseV2, Errors>> GetByIdAsyncV2(int id, CancellationToken cancellationToken = default)
		{
			var student = await _unitOfWork.Student.FindByInclude(x => x.Id == id, cancellationToken, ["Department"]);
			if (student is null)
				return StudentErrors.NotFound;

			var Response = student.Adapt<StudentResponseV2>();
			return Response;
		}
		public async Task<IEnumerable<StudentResponseV2>> GetAllAsync(CancellationToken cancellationToken = default)
		{
			var cacheKey = cashPrefix;
			var students = await _hybridCache.GetOrCreateAsync<IEnumerable<StudentResponseV2>>(
				cacheKey,
				async cacheEntry =>
				{
					var studentEntities = await _unitOfWork.Student.FindAllInclude(cancellationToken, ["Department"]);
					return studentEntities.Adapt<IEnumerable<StudentResponseV2>>();
				});
			return students;
		}
		public async Task<OneOf<StudentResponseV2, Errors>> CreateAsync(StudentRequest request, CancellationToken cancellationToken = default)
		{
			var existedStudent = await _unitOfWork.Student.FindByInclude(x => x.Name == request.Name
				&& x.Address == request.Address && x.Phone == request.Phone, cancellationToken);
			if (existedStudent is not null)
				return StudentErrors.Duplicate;

			var student = request.Adapt<Student>();
			await _unitOfWork.Student.CreateAsync(student, cancellationToken);
			await _hybridCache.RemoveAsync(cashPrefix, cancellationToken);
			var studentWithDepartment = await _unitOfWork.Student.FindByInclude(x => x.Id == student.Id, cancellationToken, new[] { "Department" });
			var response = studentWithDepartment.Adapt<StudentResponseV2>();
			return response;
		}
		public async Task<OneOf<StudentResponseV2, Errors>> UpdateAsync(int id, StudentRequest request, CancellationToken cancellationToken = default)
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
			await _hybridCache.RemoveAsync(cashPrefix, cancellationToken);

			var studentWithDepartment = await _unitOfWork.Student.FindByInclude(x => x.Id == student.Id, cancellationToken, ["Department"]);
			var response = studentWithDepartment.Adapt<StudentResponseV2>();
			return response;
		}
		public async Task<OneOf<Errors?>> DeleteAsync(int id, CancellationToken cancellationToken = default)
		{
			var student = await _unitOfWork.Student.GetByIdAsync(id, cancellationToken);
			if (student is null)
				return StudentErrors.NotFound;

			await _unitOfWork.Student.DeleteAsync(student, cancellationToken);
			await _hybridCache.RemoveAsync(cashPrefix, cancellationToken);

			return null;
		}
	}
}
