namespace School.Application.DTOs.Authentication;
using School.Application.Const;
public class ResetPasswordRequestValidator : AbstractValidator<_ResetPasswordRequest>
{
	public ResetPasswordRequestValidator()
	{
		RuleFor(x => x.Email).NotEmpty().EmailAddress();
		RuleFor(x => x.Code).NotEmpty();
		RuleFor(x => x.newPassword)
			.Matches(RegexPattern.StrongPassword)
			.WithMessage("Password must contains atleast 8 digits, one Uppercase,one Lowercase and NunAlphanumeric");


	}

}
