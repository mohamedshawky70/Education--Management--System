namespace School.Application.DTOs.User;

public class RoleRequestValidator : AbstractValidator<RoleRequest>
{
	public RoleRequestValidator()
	{
		RuleFor(x => x.name)
			.NotEmpty()
			.Length(3, 200);
	}
}
