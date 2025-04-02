using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Application.DTOs.Teacher
{
    public record TeacherResponse
    (
        int Id,
        string Name,
        string Email,
        int SubjectId,
        IEnumerable<string> SubjectName
    );
}
