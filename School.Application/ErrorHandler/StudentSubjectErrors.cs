namespace School.Application.ErrorHandler
{
	public static class StudentSubjectErrors
	{
		public static readonly Errors Dublicate = new Errors("StudentSubject.Duplicate", "This StudentSubject already existed.", StatusCodes.Status409Conflict);
	}
}
