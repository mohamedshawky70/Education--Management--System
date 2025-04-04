using School.Application.Const;

namespace School.Application.DTOs.Authentication;

public class ChangePasswordRequestValidation : AbstractValidator<ChangePasswordRequest>
{
	public ChangePasswordRequestValidation()
	{
		RuleFor(x => x.currentPassword).NotEmpty();

		RuleFor(x => x.newPassword).NotEmpty()
			.NotEqual(x => x.currentPassword)
			.WithMessage("New password can't be the same current password")
			.Matches(RegexPattern.StrongPassword)
			.WithMessage("Password must contains atleast 8 digits, one Uppercase,one Lowercase and NunAlphanumeric");

	}
}
