using School.Application.DTOs.Assignment;

namespace School.Application.Validation.Assignment
{
	public class SubjectResponseValidator : AbstractValidator<AssignmentRequest>
	{
		public SubjectResponseValidator()
		{
			RuleFor(x => x.Title).MaximumLength(100)
				.WithMessage("Maximum length of Title is 100 char");
		}
	}
}
