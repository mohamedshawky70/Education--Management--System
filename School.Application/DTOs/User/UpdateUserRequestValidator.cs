using School.Application.DTOs.User;

namespace SurveyBasket.API.DTOs.User;

public class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequest>
{
	public UpdateUserRequestValidator()
	{
		RuleFor(x => x.FirstName)
			.NotEmpty()
			.Length(3, 100);
		RuleFor(x => x.LastName)
			.NotEmpty()
			.Length(3, 100);
		RuleFor(x => x.Email)
			.NotEmpty()
			.EmailAddress();
		RuleFor(x => x.Roles)
			.NotEmpty()
			.NotNull();
		RuleFor(x => x.Roles)
			.Must(x => x.Distinct().Count() == x.Count)
			.WithMessage("You can't add duplicated role for the same user")
			.When(x => x.Roles != null);
	}
}
