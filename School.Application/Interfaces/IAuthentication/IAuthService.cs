

using OneOf;
using School.Application.DTOs.Authentication;
using School.Application.ErrorHandler;

namespace School.Application.Interfaces.IAuthentication;

public interface IAuthService
{
	Task<OneOf<AuthResponse?, Errors>> GetTokenAsync(string email, string password, CancellationToken cancellationToken = default);
	Task<OneOf<AuthResponse?, Errors>> GetRefreshTokenAsync(string token, string refreshToken, CancellationToken cancellationToken = default);
	Task<OneOf<bool, Errors>> RevokeRefreshTokenAsync(string token, string refreshToken, CancellationToken cancellationToken = default);
	Task<OneOf<string?, Errors>> RegisterAsync( _RegisterRequest request, CancellationToken cancellationToken = default);
	Task<OneOf<AuthResponse?, Errors>> ConfirmEmailAsync(ConfirmEmailRequest request);
	Task<OneOf<string, Errors>> ResendConfirmationEmailAsync(_ResendConfirmationEmailRequest request);
	Task<OneOf<string, Errors>> SendResetPasswordAsync(ForgetPasswordRequest request);
	Task<OneOf<string, Errors>> ResetPasswordAsync(_ResetPasswordRequest request);
}
