using School.Application.Const;
using School.Application.DTOs.Assignment;
using School.Application.DTOs.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Application.Validation.Assignment
{
    public class AssignmentRequestValidator:AbstractValidator<AssignmentRequest>
    {
		public AssignmentRequestValidator()
		{
			RuleFor(x => x.Title).MaximumLength(100)
				.WithMessage("Maximum length of Title is 100 char");
		}
	}
}
