namespace School.Application.ErrorHandler
{
	public static class TeacherErrors
	{
		public static readonly Errors NotFound = new Errors("Teacher.NotFound", "Teacher is NotFound.", StatusCodes.Status404NotFound);
		//public static readonly Errors Dublicate = new Errors("Teacher.Duplicate", "This Teacher already has this subject assigned.", StatusCodes.Status409Conflict);
	}
}
