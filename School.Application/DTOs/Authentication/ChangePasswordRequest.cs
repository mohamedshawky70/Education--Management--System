namespace School.Application.DTOs.Authentication;

public record ChangePasswordRequest
(
	string currentPassword,
	string newPassword
);
