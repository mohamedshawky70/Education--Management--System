namespace School.Application.ErrorHandler;

public static class RoleErrors
{
	public static readonly Errors DuplicateRole = new Errors("Role.DuplicateUser", "Role with the same name already existed", StatusCodes.Status409Conflict);
	public static readonly Errors NotFound = new Errors("Role.NotFound", "Role is NotFound", StatusCodes.Status404NotFound);

}
