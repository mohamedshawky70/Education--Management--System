namespace School.Application.ErrorHandler
{
	public static class SubjectErrors
	{
		public static readonly Errors NotFound = new Errors("Subject.NotFound", "Subject is NotFound.", StatusCodes.Status404NotFound);
		public static readonly Errors Dublicate = new Errors("Subject.Duplicate", "This subject already existed.", StatusCodes.Status409Conflict);
		public static readonly Errors Enrolled = new Errors("Subject.Enrolled", "This subject already Enrolled by student.", StatusCodes.Status409Conflict);
	}
}
