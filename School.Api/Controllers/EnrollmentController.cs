using Microsoft.AspNetCore.Mvc;
using School.Application.DTOs.Student;
using School.Application.Interfaces.IServices;

namespace School.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class EnrollmentController(IEnrollmentService enrollmentService) : ControllerBase
	{
		private readonly IEnrollmentService _enrollmentService = enrollmentService;

		[HttpGet("{id}")]
		public async Task<IActionResult> GetStudentSubjects([FromRoute] int id, CancellationToken cancellationToken)
		{
			var response = await _enrollmentService.GetStudentSubjectsAsync(id, cancellationToken);
			return response.Match(Ok, error => Problem(error.Code, error.Description, error.StatusCode));
		}
		[HttpGet("")]
		public async Task<IActionResult> GetStudentsSubjects(CancellationToken cancellationToken)
		{
			var response = await _enrollmentService.GetStudentsSubjectsAsync(cancellationToken);
			return Ok(response);
		}
		[HttpPost("create-student-subject")]
		public async Task<IActionResult> CreateStudentSubject([FromBody] StudentSubjectRequest request, CancellationToken cancellationToken)
		{
			var response = await _enrollmentService.CreateStudentSubjectAsync(request, cancellationToken);
			if (response.AsT0 == null) return Ok();
			else return response.Match(error => Problem(error!.Code, error.Description, error.StatusCode));
		}
		[HttpDelete("drop-student-from-subject")]
		public async Task<IActionResult> DropStudentFromSubject([FromBody] StudentSubjectRequest request, CancellationToken cancellationToken)
		{
			var response = await _enrollmentService.DropStudentFromSubjectAsync(request, cancellationToken);
			if (response.AsT0 == null) return Ok();
			else return response.Match(error => Problem(error!.Code, error.Description, error.StatusCode));
		}
	}
}
