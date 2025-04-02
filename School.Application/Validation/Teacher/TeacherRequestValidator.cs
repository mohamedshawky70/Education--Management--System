
using School.Application.DTOs.Teacher;

namespace School.Application.Validation.Teacher
{
	public class TeacherRequestValidator : AbstractValidator<TeacherRequest>
	{
		public TeacherRequestValidator()
		{
			RuleFor(x => x.Name).MaximumLength(100)
				.WithMessage("Maximum length of Title is 100 char");
			RuleFor(x => x.Email).EmailAddress()
				.WithMessage("Email is not validation");
		}
	}
}
