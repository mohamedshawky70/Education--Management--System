using Microsoft.AspNetCore.Mvc;


namespace School.Application.Interfaces.IUser;

public interface IUserService
{
	//Task<PaginationList<UserResponse>> GetAllAsync(FilterRequest filter, CancellationToken cancellationToken = default);
	Task<OneOf<UserResponse, Errors>> GetByIdAsync(string id, CancellationToken cancellationToken = default);
	Task<OneOf<UserResponse, Errors>> CreateAsync(CreateUserRequest request, CancellationToken cancellationToken = default);
	Task<OneOf<UserResponse, Errors>> UpdateAsync(string id, UpdateUserRequest request, CancellationToken cancellationToken = default);
	Task<OneOf<UserResponse, Errors>> ToggleStatus(string id);
	Task<OneOf<NoContentResult, Errors>> UnLock(string id, CancellationToken cancellationToken);
	//Task<object?> GetAllAsync(FilterRequest filter, CancellationToken cancellationToken);
}
