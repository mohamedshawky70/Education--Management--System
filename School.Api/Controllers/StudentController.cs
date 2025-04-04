using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using School.Application.DTOs.Student;
using School.Application.Interfaces.IServices;

namespace School.Api.Controllers
{
	[ApiVersion(1, Deprecated = true)]
	[ApiVersion(2)]
	[Route("api/v{v:apiVersion}/[controller]")]
	[ApiController]
	[EnableRateLimiting("Token")]

	public class StudentController(IStudentService studentService) : ControllerBase
	{
		private readonly IStudentService _studentService = studentService;

		[HttpGet("{id}")]
		[ApiVersion(1)]
		public async Task<IActionResult> GetByIdV1([FromRoute] int id, CancellationToken cancellationToken)
		{
			var response = await _studentService.GetByIdAsyncV1(id, cancellationToken);
			return response.Match(Ok, error => Problem(error.Code, error.Description, error.StatusCode));
		}
		[HttpGet("{id}")]
		[ApiVersion(2)]
		public async Task<IActionResult> GetByIdV2([FromRoute] int id, CancellationToken cancellationToken)
		{
			var response = await _studentService.GetByIdAsyncV2(id, cancellationToken);
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
				return CreatedAtAction(nameof(GetByIdV2), new { id = response.AsT0.Id }, response.AsT0);
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

	}
}
