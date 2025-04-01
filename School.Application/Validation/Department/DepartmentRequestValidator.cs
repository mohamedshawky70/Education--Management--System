using School.Application.Const;
using School.Application.DTOs.Student;

namespace School.Application.Validation.Student
{
	public class DepartmentRequestValidator : AbstractValidator<StudentRequest>
	{
		public DepartmentRequestValidator()
		{
			RuleFor(x => x.Name).MaximumLength(100)
				.WithMessage("Maximum length of name is 100 char");
			RuleFor(x => x.Name).Matches(RegexPattern.EnglishLetter)
				.WithMessage("English letter only");
		}
	}
}
