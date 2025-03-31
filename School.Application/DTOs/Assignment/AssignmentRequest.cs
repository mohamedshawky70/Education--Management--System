using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Application.DTOs.Assignment
{
    public record AssignmentRequest
	(
		 string Title,
		 DateTime DueDate,
		 byte MaxScore,
		 int SubjectId,
		 int StudentId
	);
}
