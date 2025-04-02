using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Application.DTOs.Teacher
{
    public record TeacherRequest
    (
		string Name,
		string Email,
		int SubjectId
	);
}
