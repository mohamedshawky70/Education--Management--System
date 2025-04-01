using Microsoft.AspNetCore.Mvc;
using School.Application.DTOs.Assignment;
using School.Application.Interfaces.IServices;

namespace School.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AssignmentController : ControllerBase
	{
		private readonly IAssignmentService _assignmentService;

		public AssignmentController(IAssignmentService assignmentService)
		{
			_assignmentService = assignmentService;
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById([FromRoute] int id, CancellationToken cancellationToken)
		{
			var response = await _assignmentService.GetByIdAsync(id, cancellationToken);
			return response.Match(Ok, error => Problem(error.Code, error.Description, error.StatusCode));
		}

		[HttpGet("")]
		public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
		{
			var response = await _assignmentService.GetAllAsync(cancellationToken);
			return Ok(response);
		}

		[HttpPost("")]
		public async Task<IActionResult> Create([FromBody] AssignmentRequest request, CancellationToken cancellationToken)
		{
			var response = await _assignmentService.CreateAsync(request, cancellationToken);
			if (response.IsT0)//IsTo ===>Is Assignment
				return CreatedAtAction(nameof(GetById), new { id = response.AsT0.Id }, response.AsT0);//AsT0 ====> Assignment 
			return response.Match(_ => null!, error => Problem(error!.Code, error.Description, error.StatusCode));
		}
		[HttpPut("{id}")]
		public async Task<IActionResult> Update([FromRoute] int id, [FromBody] AssignmentRequest request, CancellationToken cancellationToken)
		{
			var response = await _assignmentService.UpdateAsync(id, request, cancellationToken);
			if (response.IsT0)//IsTo ===>Is Assignment
				return CreatedAtAction(nameof(GetById), new { id = response.AsT0.Id }, response.AsT0);//AsT0 ====> Assignment 
			return response.Match(_ => null!, error => Problem(error!.Code, error.Description, error.StatusCode));
		}
		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken cancellationToken)
		{
			var response = await _assignmentService.DeleteAsync(id, cancellationToken);
			if (response.AsT0 == null) return Ok();
			else return response.Match(error => Problem(error!.Code, error.Description, error.StatusCode));
		}
	}
}
