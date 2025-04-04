namespace School.Application.Interfaces.IAuthentication
{
	public interface IJwtProvider
	{
		(string taken, int expireIn) GenerateTaken(ApplicationUser user, IEnumerable<string> roles);
		//Refresh Taken
		string? ValidateTaken(string taken);
	}

}