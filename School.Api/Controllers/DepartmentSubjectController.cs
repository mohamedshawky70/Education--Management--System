using Microsoft.AspNetCore.Mvc;
using School.Application.DTOs.DepartmentSubject;
using School.Application.Interfaces.IServices;

namespace School.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DepartmentSubjectController(IDepartmentSubjectService departmentSubject) : ControllerBase
	{
		private readonly IDepartmentSubjectService _departmentSubject = departmentSubject;

		[HttpGet("{id}")]
		public async Task<IActionResult> GetStudentSubjects([FromRoute] int id, CancellationToken cancellationToken)
		{
			var response = await _departmentSubject.GetDepartmentSubjectsAsync(id, cancellationToken);
			return response.Match(Ok, error => Problem(error.Code, error.Description, error.StatusCode));
		}
		[HttpGet("")]
		public async Task<IActionResult> GetStudentsSubjects(CancellationToken cancellationToken)
		{
			var response = await _departmentSubject.GetDepartmentsSubjectsAsync(cancellationToken);
			return Ok(response);
		}
		[HttpPost("create-department-subject")]
		public async Task<IActionResult> CreateStudentSubject([FromBody] DepartmentSubjectRequest request, CancellationToken cancellationToken)
		{
			var response = await _departmentSubject.CreateDepartmentSubjectAsync(request, cancellationToken);
			if (response.AsT0 == null) return Ok();
			else return response.Match(error => Problem(error!.Code, error.Description, error.StatusCode));
		}
		[HttpDelete("drop-department-from-subject")]
		public async Task<IActionResult> DropDepartmentFromSubject([FromBody] DepartmentSubjectRequest request, CancellationToken cancellationToken)
		{
			var response = await _departmentSubject.DropDepartmentFromSubjectAsync(request, cancellationToken);
			if (response.AsT0 == null) return Ok();
			else return response.Match(error => Problem(error!.Code, error.Description, error.StatusCode));
		}
	}
}
