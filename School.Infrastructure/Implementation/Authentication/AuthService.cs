
using Hangfire;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.WebUtilities;
using School.Application.Interfaces.IAuthentication;
using SurveyBasket.API.Const;
using System.Security.Cryptography;
using System.Text;

namespace School.Infrastructure.Implementation.Authentication;
public class AuthService : IAuthService
{
	private readonly UserManager<ApplicationUser> _userManager;
	private readonly RoleManager<IdentityRole> _roleManager;
	private readonly SignInManager<ApplicationUser> _signInManager;
	private readonly IJwtProvider _jwtProvider;
	private readonly IEmailSender _emailSender;
	private readonly IHttpContextAccessor _httpContextAccessor;

	private readonly int _refreshTokenExpiryDays = 14;
	public AuthService(UserManager<ApplicationUser> userManager, IJwtProvider jwtProvider, SignInManager<ApplicationUser> signInManager, IEmailSender emailSender, IHttpContextAccessor httpContextAccessor, RoleManager<IdentityRole> roleManager)
	{
		_userManager = userManager;
		_jwtProvider = jwtProvider;
		_signInManager = signInManager;
		_emailSender = emailSender;
		_httpContextAccessor = httpContextAccessor;
		_roleManager = roleManager;
	}
	public async Task<OneOf<AuthResponse?, Errors>> GetTokenAsync(string email, string password, CancellationToken cancellationToken = default)
	{
		//check email
		var user = await _userManager.FindByEmailAsync(email);
		if (user is null)
			return UserErrors.InvalidCredential;
		//check password
		var result = await _signInManager.PasswordSignInAsync(user, password, false, true);
		if (result.Succeeded)
		{
			//send roles of user to taken for front end 
			var userRoles = await _userManager.GetRolesAsync(user);
			//generate JWT taken
			var (taken, expiresIn) = _jwtProvider.GenerateTaken(user, userRoles);

			//generate refresh token
			var refreshToken = GenerateRefreshToken();
			var refreshTokenExpiration = DateTime.UtcNow.AddDays(_refreshTokenExpiryDays);
			user.RefreshTokens.Add(new RefreshToken
			{
				Token = refreshToken,
				ExpiresIn = refreshTokenExpiration
			});
			await _userManager.UpdateAsync(user);

			return new AuthResponse(user.Id, user.Email, user.FirstName, user.LastName, taken, expiresIn, refreshToken, refreshTokenExpiration);
		}
		// يإما اللباس غلط او مش كونفرمد
		//او لوك
		var error =
			 result.IsLockedOut
			? UserErrors.UserLockOut
			: result.IsNotAllowed
			? UserErrors.EmailNotConfirmed
			: UserErrors.InvalidCredential;
		return error;
	}
	public async Task<OneOf<AuthResponse?, Errors>> GetRefreshTokenAsync(string token, string refreshToken, CancellationToken cancellationToken = default)
	{
		var userId = _jwtProvider.ValidateTaken(token);
		//لو الاكباير ديت انتهي هيرجع نل
		if (userId is null)
			return UserErrors.InvalidRefreshToken;

		var user = await _userManager.FindByIdAsync(userId);
		//المنطق انه مش هيرجع نل لكن لا نثق ابدا في اليوزر اللعين
		if (user is null)
			return UserErrors.InvalidRefreshToken;

		if (user.LockoutEnd > DateTime.UtcNow)
			return UserErrors.UserLockOut;

		var userRefreshToken = user.RefreshTokens.SingleOrDefault(u => u.Token == refreshToken);
		if (userRefreshToken is null)
			return UserErrors.InvalidRefreshToken;
		userRefreshToken.RevokedIn = DateTime.UtcNow;//إلغي الرفرش القديمه
													 //Generate new refresh token
		var userRoles = await _userManager.GetRolesAsync(user);
		var (newToken, expiresIn) = _jwtProvider.GenerateTaken(user, userRoles);

		//generate new refresh taken
		var newRefreshToken = GenerateRefreshToken();
		var refreshTokenExpiration = DateTime.UtcNow.AddDays(_refreshTokenExpiryDays);
		user.RefreshTokens.Add(new RefreshToken
		{
			Token = newRefreshToken,
			ExpiresIn = refreshTokenExpiration
		});
		await _userManager.UpdateAsync(user);

		return new AuthResponse(user.Id, user.Email, user.FirstName, user.LastName, newToken, expiresIn, newRefreshToken, refreshTokenExpiration);

	}
	public async Task<OneOf<bool, Errors>> RevokeRefreshTokenAsync(string token, string refreshToken, CancellationToken cancellationToken = default)
	{
		var userId = _jwtProvider.ValidateTaken(token);
		//لو الاكباير ديت انتهي هيرجع نل
		if (userId is null)
			return UserErrors.InvalidRefreshToken_Token;

		var user = await _userManager.FindByIdAsync(userId);
		//المنطق انه مش هيرجع نل لكن لا نثق ابدا في اليوزر اللعين
		if (user is null)
			return UserErrors.InvalidRefreshToken_Token;

		var userRefreshToken = user.RefreshTokens.SingleOrDefault(u => u.Token == refreshToken);
		if (userRefreshToken is null)
			return UserErrors.InvalidRefreshToken_Token;
		userRefreshToken.RevokedIn = DateTime.UtcNow;//إلغي الرفرش القديمه
		await _userManager.UpdateAsync(user);
		return true;
	}
	public async Task<OneOf<string?, Errors>> RegisterAsync(_RegisterRequest request, CancellationToken cancellationToken = default)
	{
		var email = await _userManager.FindByEmailAsync(request.Email);
		if (email is not null)
			return UserErrors.DuplicateUser;
		var user = new ApplicationUser()
		{
			Email = request.Email,
			UserName = request.Email,
			FirstName = request.FirstName,
			LastName = request.LastName
		};
		var result = await _userManager.CreateAsync(user, request.Password);
		if (result.Succeeded)
		{
			//Generate code to send it in email to user to confirmed
			var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
			code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

			//Start Send Email
			await SendEmailConfirmation(user, code);
			//End Send Email
			return UserErrors.SendEmail;
			//return code;
		}
		var error = result.Errors.First();
		return new Errors(error.Code, error.Description, StatusCodes.Status400BadRequest);
	}
	public async Task<OneOf<AuthResponse?, Errors>> ConfirmEmailAsync(ConfirmEmailRequest request)
	{
		var user = await _userManager.FindByIdAsync(request.UserId);
		if (user is null)
			return UserErrors.InvalidCode;
		if (user.EmailConfirmed)
			return UserErrors.DuplicateConfirmed;

		var code = request.Code;
		try
		{
			code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code)); //فك تشفيرته
		}
		catch (Exception)
		{
			return UserErrors.InvalidCode;
		}
		var result = await _userManager.ConfirmEmailAsync(user, code); //verify
		if (result.Succeeded)
		{
			await _userManager.AddToRoleAsync(user, AppRoles.Member);
			return UserErrors.Success;
		}

		var error = result.Errors.First();
		return new Errors(error.Code, error.Description, StatusCodes.Status400BadRequest);
	}
	public async Task<OneOf<string, Errors>> ResendConfirmationEmailAsync(_ResendConfirmationEmailRequest request)
	{
		var user = await _userManager.FindByEmailAsync(request.Email);
		if (user is null)
			return UserErrors.ResendEmail;
		if (user.EmailConfirmed)
			return UserErrors.DuplicateConfirmed;

		var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
		code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
		await SendEmailConfirmation(user, code);
		return UserErrors.ResendEmail;

	}
	public async Task<OneOf<string, Errors>> SendResetPasswordAsync(ForgetPasswordRequest request)
	{
		var user = await _userManager.FindByEmailAsync(request.Email);
		if (user is null)
			return UserErrors.ResetPassword;
		if (!user.EmailConfirmed)
			return UserErrors.EmailNotConfirmed;
		var code = await _userManager.GeneratePasswordResetTokenAsync(user);
		code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
		await SendResetPassword(user, code);
		return UserErrors.ResetPassword;

	}
	public async Task<OneOf<string, Errors>> ResetPasswordAsync(_ResetPasswordRequest request)
	{
		var user = await _userManager.FindByEmailAsync(request.Email);
		if (user is null || !user.EmailConfirmed)
			return UserErrors.InvalidCode;
		IdentityResult result;
		try
		{
			var code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.Code)); //فك تشفيرته
			result = await _userManager.ResetPasswordAsync(user, code, request.newPassword);
		}
		catch (FormatException)
		{
			result = IdentityResult.Failed(_userManager.ErrorDescriber.InvalidToken());
		}
		if (result.Succeeded)
			return UserErrors.SuccessResetPassword;

		var error = result.Errors.First();
		return new Errors(error.Code, error.Description, StatusCodes.Status401Unauthorized);
	}

	private static string GenerateRefreshToken() =>
		Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
	//Start Send Email
	private async Task SendEmailConfirmation(ApplicationUser user, string code)
	{
		var origin = _httpContextAccessor.HttpContext?.Request.Headers.Origin;// اليو ار ال اللي مبعوتلي مع الريكويست في الهيدر
		var TempPath = $"{Directory.GetCurrentDirectory()}/Templates/EmailConfirmation.html";//مكان التمبلت اللي هتتبعت
		StreamReader streamReader = new StreamReader(TempPath);//للتعامل مع هذه التمبلت
		var body = streamReader.ReadToEnd();//إقراها للاخر
		streamReader.Close();
		body = body
			.Replace("[name]", $"{user.FirstName} {user.LastName}")
			.Replace("[action_url]", $"{origin}/auth/emailConfirmation?userId={user.Id}&code={code}");//الفرونت هيعرفني شكل اليو ار ال ده مثال مش اكتر

		BackgroundJob.Enqueue(() => _emailSender.SendEmailAsync(user.Email!, "Confirm your email", body));
		await Task.CompletedTask;
	}
	private async Task SendResetPassword(ApplicationUser user, string code)
	{
		var origin = _httpContextAccessor.HttpContext?.Request.Headers.Origin;// اليو ار ال اللي مبعوتلي مع الريكويست في الهيدر
		var TempPath = $"{Directory.GetCurrentDirectory()}/Templates/ForgetPassword.html";//مكان التمبلت اللي هتتبعت
		StreamReader streamReader = new StreamReader(TempPath);//للتعامل مع هذه التمبلت
		var body = streamReader.ReadToEnd();//إقراها للاخر
		streamReader.Close();
		body = body
			.Replace("{{name}}", $"{user.FirstName} {user.LastName}")
			.Replace("{{action_url}}", $"{origin}/auth/ForgetPassword?email={user.Email}&code={code}");//الفرونت هيعرفني شكل اليو ار ال ده مثال مش اكتر

		BackgroundJob.Enqueue(() => _emailSender.SendEmailAsync(user.Email!, "Reset your password", body));
		await Task.CompletedTask;
	}
	//End Send Email
}
