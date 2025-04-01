using Microsoft.AspNetCore.Mvc;
using School.Application.DTOs.Department;
using School.Application.Interfaces.IServices;

namespace School.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DepartmentController(IDepartmentService departmentService) : ControllerBase
	{
		private readonly IDepartmentService _departmentService = departmentService;
		[HttpGet("{id}")]
		public async Task<IActionResult> GetById([FromRoute] int id, CancellationToken cancellationToken)
		{
			var response = await _departmentService.GetByIdAsync(id, cancellationToken);
			return response.Match(Ok, error => Problem(error.Code, error.Description, error.StatusCode));
		}
		[HttpGet("")]
		public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
		{
			var response = await _departmentService.GetAllAsync(cancellationToken);
			return Ok(response);
		}
		[HttpPost("")]
		public async Task<IActionResult> Create([FromBody] DepartmentRequest request, CancellationToken cancellationToken)
		{
			var response = await _departmentService.CreateAsync(request, cancellationToken);
			if (response.IsT0)
				return RedirectToAction(nameof(GetById), new { id = response.AsT0.Id });
			return response.Match(_ => null!, error => Problem(error.Code, error.Description, error.StatusCode));
		}
		[HttpPut("{id}")]
		public async Task<IActionResult> Update([FromRoute] int id, [FromBody] DepartmentRequest request, CancellationToken cancellationToken)
		{
			var response = await _departmentService.UpdateAsync(id, request, cancellationToken);
			return response.Match(Ok, error => Problem(error.Code, error.Description, error.StatusCode));
		}
		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken cancellationToken)
		{
			var response = await _departmentService.DeleteAsync(id, cancellationToken);
			if (response.AsT0 == null) return Ok();
			else return response.Match(error => Problem(error!.Code, error.Description, error.StatusCode));
		}

	}
}
