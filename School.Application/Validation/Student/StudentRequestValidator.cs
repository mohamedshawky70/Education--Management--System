using School.Application.Const;
using School.Application.DTOs.Student;

namespace School.Application.Validation.Student
{
	public class StudentRequestValidator : AbstractValidator<StudentRequest>
	{
		public StudentRequestValidator()
		{
			RuleFor(x => x.Name).MaximumLength(100)
				.WithMessage("Maximum length of name is 100 char");
			RuleFor(x => x.Name).Matches(RegexPattern.EnglishLetter)
				.WithMessage("English letter only");
		}
	}
}
