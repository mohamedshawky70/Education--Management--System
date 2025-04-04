using School.Application.Interfaces.IAuthentication;
using SurveyBasket.API.DTOs.User;

namespace SurveyBasket.API.Services;

public class RoleService : IRoleService
{
	private readonly RoleManager<IdentityRole> _roleManager;
	public RoleService(RoleManager<IdentityRole> roleManager)
	{
		_roleManager = roleManager;
	}
	public async Task<IEnumerable<RoleResponse>> GetAllAsync(CancellationToken cancellationToken = default)
	{
		var roles = await _roleManager.Roles
			.ProjectToType<RoleResponse>()
			.ToListAsync(cancellationToken);
		return roles;
	}
	public async Task<OneOf<Successes, Errors>> CreateAsync(RoleRequest request, CancellationToken cancellationToken = default)
	{
		var roleIsExisted = await _roleManager.RoleExistsAsync(request.name);
		if (roleIsExisted)
			return RoleErrors.DuplicateRole;

		await _roleManager.CreateAsync(new IdentityRole(request.name));
		return new Successes("Created successfully");
	}
	public async Task<OneOf<Successes, Errors>> UpdateAsync(string id, RoleRequest request)
	{
		var roleIsExisted = await _roleManager.Roles.AnyAsync(r => r.Name == request.name && r.Id != id);
		if (roleIsExisted)
			return RoleErrors.DuplicateRole;

		var role = await _roleManager.FindByIdAsync(id);
		if (role is null)
			return RoleErrors.NotFound;

		role.Name = request.name;
		await _roleManager.UpdateAsync(role);
		return new Successes("Updated successfully");
	}
	public async Task<OneOf<Successes, Errors>> DeleteAsync(string id)
	{
		var role = await _roleManager.FindByIdAsync(id);
		if (role is null)
			return RoleErrors.NotFound;
		await _roleManager.DeleteAsync(role); //Hard delete no recommended
		return new Successes("Deleted successfully");
	}
}
