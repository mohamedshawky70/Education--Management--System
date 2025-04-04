namespace School.Application.DTOs.Authentication;

public class LoginRequestValidator : AbstractValidator<_LoginRequest>
{
	public LoginRequestValidator()
	{
		RuleFor(x => x.email)
			.NotEmpty()
			.EmailAddress();
		RuleFor(x => x.password)
			.NotEmpty();
	}
}
