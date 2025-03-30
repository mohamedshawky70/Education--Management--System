using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Application.DTOs.Student
{
    public record StudentSubjectRequest
    (
        int StudentId,
        int SubjectId
    );
}
