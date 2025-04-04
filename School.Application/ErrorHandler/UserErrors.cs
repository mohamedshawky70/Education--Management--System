namespace School.Application.ErrorHandler;

public static class UserErrors
{
	public static readonly Errors InvalidCredential = new Errors("User.InvalidCredential", "Invalid email/password", StatusCodes.Status401Unauthorized);
	public static readonly Errors InvalidRefreshToken = new Errors("User.InvalidRefreshToken", "InvalidRefreshToken~", StatusCodes.Status401Unauthorized);
	public static readonly Errors InvalidRefreshToken_Token = new Errors("User.InvalidRefreshToken/Token", "InvalidRefreshToken/Token", StatusCodes.Status401Unauthorized);
	public static readonly Errors DuplicateUser = new Errors("User.DuplicateUser", "User with the same email already existed", StatusCodes.Status409Conflict);
	public static readonly Errors EmailNotConfirmed = new Errors("User.EmailNotConfirmed", "Email not confirmed", StatusCodes.Status401Unauthorized);
	public static readonly Errors InvalidCode = new Errors("User.InvalidCode", "Invalid Code", StatusCodes.Status401Unauthorized);
	public static readonly Errors DuplicateConfirmed = new Errors("User.DuplicateConfirmed", "This email already confirmed", StatusCodes.Status400BadRequest);
	public static readonly Errors Success = new Errors("User.Success", "Email confirmed successfully", StatusCodes.Status200OK);
	public static readonly Errors SuccessResetPassword = new Errors("User.SuccessResetPassword", "Password reset successfully", StatusCodes.Status200OK);
	public static readonly Errors ResendEmail = new Errors("User.ResendEmail", "Email confirmation resend successfully", StatusCodes.Status200OK);
	public static readonly Errors SendEmail = new Errors("User.SendEmail", "Email confirmation Send successfully", StatusCodes.Status200OK);
	public static readonly Errors ResetPassword = new Errors("User.ResetPassword", "Code Send successfully", StatusCodes.Status200OK);
	public static readonly Errors UserIsDisable = new Errors("User.UserIsDisable", "User Is Disable", StatusCodes.Status401Unauthorized);
	public static readonly Errors UserLockOut = new Errors("User.UserLockOut", "User Is LockOut", StatusCodes.Status401Unauthorized);
	public static readonly Errors UserNotFound = new Errors("User.NotFound", " User Not Found", StatusCodes.Status401Unauthorized);
	public static readonly Errors InvalidRole = new Errors("User.InvalidRole", "Invalid Role", StatusCodes.Status400BadRequest);
}
