using Microsoft.AspNetCore.Mvc;
using School.Application.DTOs.Student;
using School.Application.Interfaces.IServices;

namespace School.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class StudentController(IStudentService studentService) : ControllerBase
	{
		private readonly IStudentService _studentService = studentService;
		[HttpGet("{id}")]
		public async Task<IActionResult> GetById([FromRoute] int id, CancellationToken cancellationToken)
		{
			var response = await _studentService.GetByIdAsync(id, cancellationToken);
			return response.Match(Ok, error => Problem(error.Code, error.Description, error.StatusCode));
		}
		[HttpGet("")]
		public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
		{
			var response = await _studentService.GetAllAsync(cancellationToken);
			return Ok(response);
		}
		[HttpPost("")]
		public async Task<IActionResult> Create([FromBody] StudentRequest request, CancellationToken cancellationToken)
		{
			var response = await _studentService.CreateAsync(request, cancellationToken);
			if (response.IsT0)
				return CreatedAtAction(nameof(GetById), new { id = response.AsT0.Id }, response.AsT0);
			return response.Match(_ => null!, error => Problem(error.Code, error.Description, error.StatusCode));
		}
		[HttpPut("{id}")]
		public async Task<IActionResult> Update([FromRoute] int id, [FromBody] StudentRequest request, CancellationToken cancellationToken)
		{
			var response = await _studentService.UpdateAsync(id, request, cancellationToken);
			return response.Match(Ok, error => Problem(error.Code, error.Description, error.StatusCode));
		}
		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken cancellationToken)
		{
			var response = await _studentService.DeleteAsync(id, cancellationToken);
			if (response.AsT0 == null) return Ok();
			else return response.Match(error => Problem(error!.Code, error.Description, error.StatusCode));
		}
		[HttpPost("student-subject")]
		public async Task<IActionResult> CreateStudentSubject([FromBody] StudentSubjectRequest request, CancellationToken cancellationToken)
		{
			var response = await _studentService.CreateStudentSubjectAsync(request, cancellationToken);
			if (response.AsT0 == null) return Ok();
			else return response.Match(error => Problem(error!.Code, error.Description, error.StatusCode));
		}
		[HttpGet("student-subjects/{id}")]
		public async Task<IActionResult> GetStudentSubjects([FromRoute] int id, CancellationToken cancellationToken)
		{
			var response = await _studentService.GetStudentSubjectsAsync(id, cancellationToken);
			return response.Match(Ok, error => Problem(error.Code, error.Description, error.StatusCode));
		}
		[HttpDelete("drop-student-from-subject")]
		public async Task<IActionResult> DropStudentFromSubject([FromBody] StudentSubjectRequest request, CancellationToken cancellationToken)
		{
			var response = await _studentService.DropStudentFromSubjectAsync(request, cancellationToken);
			if (response.AsT0 == null) return Ok();
			else return response.Match(error => Problem(error!.Code, error.Description, error.StatusCode));
		}

	}
}
