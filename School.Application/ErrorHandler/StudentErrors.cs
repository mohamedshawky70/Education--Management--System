using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace School.Application.ErrorHandler
{
    public static class StudentErrors
    {
		public static readonly Errors Duplicate = new Errors("Student.Duplicate", "Student is already existed",StatusCodes.Status409Conflict);
		public static readonly Errors NotFound = new Errors("Student.NotFound", "Student is NotFound", StatusCodes.Status404NotFound);
	}
}
