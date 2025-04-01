using School.Application.DTOs.Subject;

namespace School.Application.Validation.Subject
{
	public class SubjectRequestValidator : AbstractValidator<SubjectRequest>
	{
		public SubjectRequestValidator()
		{
			RuleFor(x => x.Name).MaximumLength(100)
				.WithMessage("Maximum length of Title is 100 char");
		}
	}
}
