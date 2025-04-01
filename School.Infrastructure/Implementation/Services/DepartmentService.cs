using School.Application.DTOs.Department;

namespace School.Infrastructure.Implementation.Services
{
	public class DepartmentService(IUnitOfWork unitOfWork) : IDepartmentService
	{
		private readonly IUnitOfWork _unitOfWork = unitOfWork;
		public async Task<OneOf<DepartmentResponse, Errors>> GetByIdAsync(int id, CancellationToken cancellationToken = default)
		{
			var department = await _unitOfWork.Department.GetByIdAsync(id, cancellationToken);
			if (department is null)
				return DepartmentErrors.NotFound;

			var Response = department.Adapt<DepartmentResponse>();
			return Response;
		}
		public async Task<IEnumerable<DepartmentResponse>> GetAllAsync(CancellationToken cancellationToken = default)
		{
			var students = await _unitOfWork.Department.GetAllAsync(cancellationToken);
			var response = students.Adapt<IEnumerable<DepartmentResponse>>();
			return response;
		}
		public async Task<OneOf<DepartmentResponse, Errors>> CreateAsync(DepartmentRequest request, CancellationToken cancellationToken = default)
		{
			var existedDepartment = await _unitOfWork.Department.FindByInclude(x => x.Name == request.Name);
			if (existedDepartment is not null)
				return DepartmentErrors.Duplicate;

			var department = request.Adapt<Department>();
			await _unitOfWork.Department.CreateAsync(department, cancellationToken);
			var response = department.Adapt<DepartmentResponse>();
			return response;
		}
		public async Task<OneOf<DepartmentResponse, Errors>> UpdateAsync(int id, DepartmentRequest request, CancellationToken cancellationToken = default)
		{
			var department = await _unitOfWork.Department.GetByIdAsync(id, cancellationToken);
			if (department is null)
				return DepartmentErrors.NotFound;

			//Map data in object to [data] in object
			var newDepartment = request.Adapt(department);
			await _unitOfWork.Department.UpdateAsync(newDepartment, cancellationToken);

			var response = newDepartment.Adapt<DepartmentResponse>();
			return response;
		}
		public async Task<OneOf<Errors?>> DeleteAsync(int id, CancellationToken cancellationToken = default)
		{
			var department = await _unitOfWork.Department.GetByIdAsync(id, cancellationToken);
			if (department is null)
				return StudentErrors.NotFound;
			var isEmptyDepartment = await _unitOfWork.Student.FindByInclude(x => x.DepartmentId == id);
			if(isEmptyDepartment is not null)
				return DepartmentErrors.NotEmpty;

			await _unitOfWork.Department.DeleteAsync(department, cancellationToken);
			return null;
		}
	}
}
