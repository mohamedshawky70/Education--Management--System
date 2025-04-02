namespace School.Application.ErrorHandler
{
	public static class TeacherErrors
	{
		public static readonly Errors NotFound = new Errors("Teacher.NotFound", "Teacher is NotFound.", StatusCodes.Status404NotFound);
		public static readonly Errors Duplicate = new Errors("Teacher.Duplicate", "This Teacher already existed.", StatusCodes.Status409Conflict);
		public static readonly Errors AssignedToSubject = new Errors("AssignedToSubject.Duplicate", "This Teacher assigned to subject.", StatusCodes.Status409Conflict);
	}
}
