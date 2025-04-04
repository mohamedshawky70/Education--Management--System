


namespace School.Api.Controllers
{
	[Route("[controller]")]
	[ApiController]
	[EnableRateLimiting("IpLimiter")]
	public class AuthController : ControllerBase
	{
		private readonly IAuthService _authService;
		private readonly UserManager<ApplicationUser> _userManager;
		public AuthController(IAuthService authService, ILogger<AuthController> logger, UserManager<ApplicationUser> userManager)
		{
			_authService = authService;
			_userManager = userManager;
		}

		[HttpPost("")]
		public async Task<IActionResult> LoginAsync([FromBody] _LoginRequest request, CancellationToken cancellationToken)
		{
			var authResult = await _authService.GetTokenAsync(request.email, request.password, cancellationToken);
			//return authResult is null ?BadRequest("Invalid email or password") : Ok;ده صح واللي تحت الاصح
			return authResult.Match(
				Ok,
				error => Problem(statusCode: error.StatusCode, 
				title: error.Code, detail: error.Description)
			);
		}
		[HttpPost("refresh")]

		public async Task<IActionResult> RefreshAsync([FromBody] RefreshTakenRequest request, CancellationToken cancellationToken)
		{
			var authResult = await _authService.GetRefreshTokenAsync(request.Taken, request.RefreshTaken, cancellationToken);
			return authResult.Match(
				Ok,
				error => Problem(statusCode: error.StatusCode,
				title: error.Code, detail: error.Description)
			);
		}

		[HttpPut("revoke-refresh-token")]

		public async Task<IActionResult> RevokeRefreshAsync([FromBody] RefreshTakenRequest request, CancellationToken cancellationToken)
		{
			var isRevoked = await _authService.RevokeRefreshTokenAsync(request.Taken, request.RefreshTaken, cancellationToken);
			return isRevoked.Match(
				isRevoked => Ok("Refresh token revoked successfully"),
				error => Problem(statusCode: StatusCodes.Status400BadRequest,
				title: error.Code, detail: error.Description)
			);
		}

		[DisableRateLimiting]
		[HttpPost("register")]
		public async Task<IActionResult> RegisterAsync([FromBody] _RegisterRequest request, CancellationToken cancellationToken)
		{
			var Result = await _authService.RegisterAsync(request, cancellationToken);
			return Result.Match(
				Result => Ok(Result),
				error => Problem(statusCode: error.StatusCode, //لو رجع error
				title: error.Code, detail: error.Description)
			);
		}

		[HttpPost("confirm-email")]
		public async Task<IActionResult> ConfirmEmailAsync([FromBody] ConfirmEmailRequest request, CancellationToken cancellationToken)
		{
			var result = await _authService.ConfirmEmailAsync(request);
			return result.Match(
				result => Ok(result),
				error => Problem(statusCode: error.StatusCode,
				title: error.Code, detail: error.Description)
			);
		}
		[HttpPost("resend-confirmation-email")]
		public async Task<IActionResult> ResendConfirmationEmailAsync([FromBody] _ResendConfirmationEmailRequest request, CancellationToken cancellationToken)
		{
			var result = await _authService.ResendConfirmationEmailAsync(request);
			return result.Match(
				result => Ok(result),
				error => Problem(statusCode: error.StatusCode,
				title: error.Code, detail: error.Description)
			);
		}
		[HttpPost("forget-password")]
		public async Task<IActionResult> ForgetPassword([FromBody] ForgetPasswordRequest request)
		{
			var result = await _authService.SendResetPasswordAsync(request);
			return result.Match(
				result => Ok(result),
				error => Problem(statusCode: error.StatusCode,
				title: error.Code, detail: error.Description)
			);
		}
		[HttpPost("reset-password")]
		public async Task<IActionResult> ResetPassword([FromBody] _ResetPasswordRequest request)
		{
			var result = await _authService.ResetPasswordAsync(request);
			return result.Match(
				result => Ok(result),
				error => Problem(statusCode: error.StatusCode,
				title: error.Code, detail: error.Description)
			);
		}

	}
}
