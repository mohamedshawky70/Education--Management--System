﻿using School.Application.Const;
using School.Application.DTOs.Department;
using School.Application.DTOs.Student;

namespace School.Application.Validation.Department
{
	public class DepartmentRequestValidator : AbstractValidator<DepartmentRequest>
	{
		public DepartmentRequestValidator()
		{
			RuleFor(x => x.Name).MaximumLength(100)
				.WithMessage("Maximum length of name is 100 char");

		}
	}
}
