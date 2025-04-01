namespace School.Application.ErrorHandler
{
	public static class DepartmentSubjectErrors
	{
		public static readonly Errors Dublicate = new Errors("Subject.Duplicate", "This subject already existed.", StatusCodes.Status409Conflict);
	}
}
