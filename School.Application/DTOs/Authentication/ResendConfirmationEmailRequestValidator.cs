using Microsoft.AspNetCore.Identity.Data;

namespace School.Application.DTOs.Authentication;

public class ResendConfirmationEmailRequestValidator : AbstractValidator<ResendConfirmationEmailRequest>
{
	public ResendConfirmationEmailRequestValidator()
	{
		RuleFor(x => x.Email).NotEmpty().EmailAddress();
	}
}
