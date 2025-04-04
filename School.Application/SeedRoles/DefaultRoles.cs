


namespace School.Application.SeedRoles;

public class DefaultRoles
{
	public async static Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
	{
		if (!roleManager.Roles.Any())
		{
			await roleManager.CreateAsync(new IdentityRole(AppRoles.Admin));
			await roleManager.CreateAsync(new IdentityRole(AppRoles.Member));
		}
	}
}
