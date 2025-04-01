using Microsoft.AspNetCore.Mvc;
using School.Application.DTOs.Subject;
using School.Application.Interfaces.IServices;

namespace School.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SubjectController : ControllerBase
	{
		private readonly ISubjectService _subjectService;

		public SubjectController(ISubjectService assignmentService)
		{
			_subjectService = assignmentService;
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById([FromRoute] int id, CancellationToken cancellationToken)
		{
			var response = await _subjectService.GetByIdAsync(id, cancellationToken);
			return response.Match(Ok, error => Problem(error.Code, error.Description, error.StatusCode));
		}

		[HttpGet("")]
		public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
		{
			var response = await _subjectService.GetAllAsync(cancellationToken);
			return Ok(response);
		}

		[HttpPost("")]
		public async Task<IActionResult> Create([FromBody] SubjectRequest request, CancellationToken cancellationToken)
		{
			var response = await _subjectService.CreateAsync(request, cancellationToken);
			if (response.IsT0)
				return CreatedAtAction(nameof(GetById), new { id = response.AsT0.Id }, response.AsT0);
			return response.Match(_ => null!, error => Problem(error!.Code, error.Description, error.StatusCode));
		}
		[HttpPut("{id}")]
		public async Task<IActionResult> Update([FromRoute] int id, [FromBody] SubjectRequest request, CancellationToken cancellationToken)
		{
			var response = await _subjectService.UpdateAsync(id, request, cancellationToken);
			if (response.IsT0)//IsTo ===>Is Assignment
				return RedirectToAction(nameof(GetById), new { id = response.AsT0.Id });//AsT0 ====> Assignment 
			return response.Match(_ => null!, error => Problem(error!.Code, error.Description, error.StatusCode));
		}
		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken cancellationToken)
		{
			var response = await _subjectService.DeleteAsync(id, cancellationToken);
			if (response.AsT0 == null) return Ok();
			else return response.Match(error => Problem(error!.Code, error.Description, error.StatusCode));
		}
	}
}
