using School.Application.DTOs.Subject;

namespace School.Application.Validation.Subject
{
	public class TeacherRequestValidator : AbstractValidator<SubjectRequest>
	{
		public TeacherRequestValidator()
		{
			RuleFor(x => x.Name).MaximumLength(100)
				.WithMessage("Maximum length of Title is 100 char");
		}
	}
}
