using Microsoft.AspNetCore.Authorization;
using School.Application.DTOs.User;
using SurveyBasket.API.Const;
using SurveyBasket.API.DTOs.User;


namespace School.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize(Roles =AppRoles.Admin)]
	public class RoleController : ControllerBase
	{
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly IRoleService _roleService;

		public RoleController(RoleManager<IdentityRole> roleManager, IRoleService roleService)
		{
			_roleManager = roleManager;
			_roleService = roleService;
		}
		[HttpGet("")]
		public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
		{
			return Ok(await _roleService.GetAllAsync());
		}
		[HttpPost("")]
		public async Task<IActionResult> Create(RoleRequest request, CancellationToken cancellationToken)
		{
			var Response = await _roleService.CreateAsync(request, cancellationToken);
			return Response.Match(
				Response => Ok(Response),
				error => Problem(statusCode: error.StatusCode,
				title: error.Code, detail: error.Description)
			);
		}
		[HttpPut("{id}")]
		public async Task<IActionResult> Update([FromRoute] string id, [FromBody] RoleRequest request)
		{
			var Response = await _roleService.UpdateAsync(id, request);
			return Response.Match(
				Response => Ok(Response),
				error => Problem(statusCode: error.StatusCode,
				title: error.Code, detail: error.Description)
			);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete([FromRoute] string id)
		{
			var Response = await _roleService.DeleteAsync(id);
			return Response.Match(
				Response => Ok(Response),
				error => Problem(statusCode: error.StatusCode,
				title: error.Code, detail: error.Description)
			);
		}
	}
}
