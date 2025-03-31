using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace School.Application.ErrorHandler
{
    public static class AssignmentErrors
	{
		public static readonly Errors Duplicate = new Errors("Assignment.Duplicate", "Assignment is already existed", StatusCodes.Status409Conflict);
		public static readonly Errors NotFound = new Errors("Assignment.NotFound", "Assignment is NotFound", StatusCodes.Status404NotFound);
		//public static readonly Errors StudentHasNotThisSubjectOrReverse = new Errors("Assignment.StudentHasNotThisSubjectOrReverse", "Student has not this subject or reverse", StatusCodes.Status404NotFound);
	}
}
