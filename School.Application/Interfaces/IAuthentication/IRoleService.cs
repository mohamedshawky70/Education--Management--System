using SurveyBasket.API.DTOs.User;

namespace School.Application.Interfaces.IAuthentication;

public interface IRoleService
{
	Task<IEnumerable<RoleResponse>> GetAllAsync(CancellationToken cancellationToken = default);
	Task<OneOf<Successes, Errors>> CreateAsync(RoleRequest request, CancellationToken cancellationToken = default);
	Task<OneOf<Successes, Errors>> UpdateAsync(string id, RoleRequest request);
	Task<OneOf<Successes, Errors>> DeleteAsync(string id);
}
