namespace School.Application.ErrorHandler
{
	public static class DepartmentErrors
	{
		public static readonly Errors Duplicate = new Errors("Department.Duplicate", "Department is already existed", StatusCodes.Status409Conflict);
		public static readonly Errors NotFound = new Errors("Department.NotFound", "Department is NotFound", StatusCodes.Status404NotFound);
		public static readonly Errors NotEmpty = new Errors("Department.NotEmpty", "Department is not empty", StatusCodes.Status404NotFound);
		public static readonly Errors DepartmentHasNotThisSubjectOrReverse = new Errors("Department.DepartmentHasNotThisSubjectOrReverse", "Department has not this subject or reverse", StatusCodes.Status404NotFound);

	}
}
