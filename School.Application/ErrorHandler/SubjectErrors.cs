using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Application.ErrorHandler
{
    public static class SubjectErrors
    {
		public static readonly Errors NotFound = new Errors("Subject.NotFound", "Subject is NotFound.", StatusCodes.Status404NotFound);
		public static readonly Errors Dublicate = new Errors("Subject.Duplicate", "This student already has this subject assigned.", StatusCodes.Status409Conflict);
	}
}
