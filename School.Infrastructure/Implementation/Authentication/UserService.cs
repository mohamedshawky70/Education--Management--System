using Microsoft.AspNetCore.Mvc;
using School.Application.DTOs.User;
using School.Application.Interfaces.IUser;

namespace School.Infrastructure.Implementation.Authentication;

public class UserService : IUserService
{
	private readonly UserManager<ApplicationUser> _userManager;
	private readonly RoleManager<IdentityRole> _roleManager;

	public UserService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
	{
		_userManager = userManager;
		_roleManager = roleManager;
	}
	//Pagination , Searching and Sorting
/*	public async Task<PaginationList<UserResponse>> GetAllAsync(FilterRequest filter, CancellationToken cancellationToken = default)
	{
		//Searching
		var users = _userManager.Users.AsQueryable();
		if (!string.IsNullOrEmpty(filter.SearchValue))
			users = users.Where(u => u.UserName!.Contains(filter.SearchValue));
		//Sorting
		//To know write the expression in OrderBy(--) install package System.Linq.Dynamic.Core and write using System.Linq.Dynamic.Core;
		if (!string.IsNullOrEmpty(filter.SortColumn))
			users = users.OrderBy($"{filter.SortColumn} {filter.SortDirection}");

		var response = users.Adapt<IEnumerable<UserResponse>>();
		//Pagination
		var userList = await PaginationList<UserResponse>.CreateAsync(response, filter.PageNumber, filter.PageSize);
		return userList;
	}
*/	public async Task<OneOf<UserResponse, Errors>> GetByIdAsync(string id, CancellationToken cancellationToken = default)
	{
		var user = await _userManager.FindByIdAsync(id);
		if (user is null)
			return UserErrors.UserNotFound;
		var userRoles = await _userManager.GetRolesAsync(user);
		//More one source (go to mapping configuration)
		var response = (user, userRoles).Adapt<UserResponse>();
		return response;
	}
	public async Task<OneOf<UserResponse, Errors>> CreateAsync(CreateUserRequest request, CancellationToken cancellationToken = default)
	{
		var emailIsExists = await _userManager.Users.AnyAsync(u => u.Email == request.Email, cancellationToken);
		if (emailIsExists)
			return UserErrors.DuplicateUser;

		var allowedRoles = await _roleManager.Roles.ToListAsync(cancellationToken: cancellationToken);
		if (request.Roles.Except(allowedRoles.Select(x => x.Name)).Any())
			return UserErrors.InvalidRole;

		var user = request.Adapt<ApplicationUser>();
		var result = await _userManager.CreateAsync(user!, request.Password);
		if (result.Succeeded)
		{
			await _userManager.AddToRolesAsync(user, request.Roles);
			var response = (user, request.Roles).Adapt<UserResponse>();
			return response;
		}
		var error = result.Errors.First();
		return new Errors(error.Code, error.Description, StatusCodes.Status400BadRequest);
	}
	public async Task<OneOf<UserResponse, Errors>> UpdateAsync(string id, UpdateUserRequest request, CancellationToken cancellationToken = default)
	{
		var emailIsExisted = await _userManager.Users.AnyAsync(u => u.Id != id && u.Email == request.Email, cancellationToken: cancellationToken);
		if (emailIsExisted)
			return UserErrors.DuplicateUser;

		var user = await _userManager.FindByIdAsync(id);
		if (user is null)
			return UserErrors.UserNotFound;

		var allowedRoles = await _roleManager.Roles.ToListAsync(cancellationToken);
		if (request.Roles.Except(allowedRoles.Select(x => x.Name)).Any())
			return UserErrors.InvalidRole;

		var newUser = request.Adapt(user);

		var result = await _userManager.UpdateAsync(newUser);
		if (result.Succeeded)
		{
			//هات الرولز القديمه
			var oldRoles = await _userManager.GetRolesAsync(user);
			//إخذف هات الرولز القديمه 
			await _userManager.RemoveFromRolesAsync(user, oldRoles);
			//ضيف علي نضافه
			var res = await _userManager.AddToRolesAsync(newUser, request.Roles);

			var response = (newUser, request.Roles).Adapt<UserResponse>();
			return response;
		}
		var error = result.Errors.First();
		return new Errors(error.Code, error.Description, StatusCodes.Status400BadRequest);
	}
	public async Task<OneOf<UserResponse, Errors>> ToggleStatus(string id)
	{
		var user = await _userManager.FindByIdAsync(id);
		if (user is null)
			return UserErrors.UserNotFound;
		user.IsDisable = !user.IsDisable;
		var result = await _userManager.UpdateAsync(user);
		if (result.Succeeded)
		{
			var Roles = await _userManager.GetRolesAsync(user);
			var response = (user, Roles).Adapt<UserResponse>();
			return response;
		}

		var error = result.Errors.First();
		return new Errors(error.Code, error.Description, StatusCodes.Status400BadRequest);
	}
	public async Task<OneOf<NoContentResult, Errors>> UnLock(string id, CancellationToken cancellationToken)
	{
		var user = await _userManager.FindByIdAsync(id);
		if (user is null)
			return UserErrors.UserNotFound;
		/*user.LockoutEnd = null;
		  await _userManager.UpdateAsync(user);*/
		//اشيك
		await _userManager.SetLockoutEndDateAsync(user, null);
		return new NoContentResult();
	}
}
