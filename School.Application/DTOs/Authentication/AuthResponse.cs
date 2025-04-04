namespace School.Application.DTOs.Authentication;

public record AuthResponse
(
	string id,
	string? Email,
	string FirstName,
	string LastName,
	string Taken,
	int ExpiresIn,
	string RefreshToken,
	DateTime RefreshTokenExpiration
);