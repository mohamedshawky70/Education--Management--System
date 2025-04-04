using Microsoft.AspNetCore.Authorization;
using School.Application.DTOs.User;
using School.Application.Interfaces.IUser;
using SurveyBasket.API.DTOs.User;

namespace School.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize(Roles =AppRoles.Admin)]
	public class UserController : ControllerBase
	{
		private readonly IUserService _userService;

		public UserController(IUserService userService)
		{
			_userService = userService;
		}
		[HttpGet("")]
		/*//Pagination , Searching and Sorting
		public async Task<IActionResult> GetAll([FromQuery] FilterRequest filter, CancellationToken cancellationToken)
		{
			return Ok(await _userService.GetAllAsync(filter, cancellationToken));
		}*/

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById([FromRoute] string id, CancellationToken cancellationToken)
		{
			var response = await _userService.GetByIdAsync(id, cancellationToken);
			return response.Match(
				response => Ok(response),//لو مرجعش error
				error => Problem(statusCode: error.StatusCode, //لو رجع error
				title: error.Code, detail: error.Description)
			);
		}
		[HttpPost("")]
		public async Task<IActionResult> CreateAsync([FromBody] CreateUserRequest request, CancellationToken cancellationToken)
		{
			var response = await _userService.CreateAsync(request, cancellationToken);
			return response.Match(
				response => Ok(response),
				error => Problem(statusCode: error.StatusCode,
				title: error.Code, detail: error.Description)
			);
		}
		[HttpPut("{id}")]
		public async Task<IActionResult> Update([FromRoute] string id, [FromBody] UpdateUserRequest request, CancellationToken cancellationToken)
		{
			var response = await _userService.UpdateAsync(id, request, cancellationToken);
			return response.Match(
				response => Ok(response),
				error => Problem(statusCode: error.StatusCode,
				title: error.Code, detail: error.Description)
			);
		}
		[HttpPut("toggle-status/{id}")]
		public async Task<IActionResult> ToggleStatus([FromRoute] string id)
		{
			var response = await _userService.ToggleStatus(id);
			return response.Match(
				response => Ok(response),
				error => Problem(statusCode: error.StatusCode,
				title: error.Code, detail: error.Description)
			);
		}

		[HttpPut("UnLock/{id}")]
		public async Task<IActionResult> UnLock([FromRoute] string id, CancellationToken cancellationToken)
		{
			var response = await _userService.UnLock(id, cancellationToken);
			return response.Match(
				Ok,
				error => Problem(statusCode: error.StatusCode,
				title: error.Code, detail: error.Description)
			);
		}
	}
}
