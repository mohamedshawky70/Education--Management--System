namespace School.Application.SeedRoles;

public class DefaultUser
{
	public async static Task SeedAdminUserAsync(UserManager<ApplicationUser> userManager)
	{
		var Admin = new ApplicationUser()
		{
			FirstName = "Mohmed",
			LastName = "Shawky",
			Email = "Admin@gmail.com",
			UserName = "Admin@gmail.com",
			EmailConfirmed = true
		};
		var user = await userManager.FindByEmailAsync(Admin.Email);
		if (user is null)
		{
			await userManager.CreateAsync(Admin, "P@ssword123");
			await userManager.AddToRoleAsync(Admin, AppRoles.Admin);
		}
	}
}
