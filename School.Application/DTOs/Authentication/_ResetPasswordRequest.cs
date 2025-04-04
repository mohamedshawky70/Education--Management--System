namespace School.Application.DTOs.Authentication;

public record _ResetPasswordRequest
(
	string Email,
	string Code,
	string newPassword
);
