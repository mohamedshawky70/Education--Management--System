
using School.Application.Interfaces.IAuthentication;

namespace School.Infrastructure.Implementation.Authentication;

public class AccountService : IAccountService
{
	private readonly UserManager<ApplicationUser> _userManager;
	private readonly IAuthService _authService;

	public AccountService(UserManager<ApplicationUser> userManager, IAuthService authService)
	{
		_userManager = userManager;
		_authService = authService;
	}
	public async Task<UserProfileResponse> Profile(string userId, CancellationToken cancellationToken = default)
	{
		//مشكلة الطريقه دي انه بيجبلك كل الداتا اللي في الجدول وانت مش محتاج الكل
		//var user= await _userManager.FindByIdAsync(User.GetUserId()!);
		var user = await _userManager.Users
			.Where(u => u.Id == userId)
			.SingleAsync(cancellationToken);
		return user.Adapt<UserProfileResponse>();
	}
	public async Task<Successes> UpdateAsync(string userId, UpdateProfileRequest request, CancellationToken cancellationToken = default)
	{
		var user = await _userManager.FindByIdAsync(userId);
		var newUser = request.Adapt(user);

		//user!.FirstName = request.FirstName;
		//user.LastName = request.LastName;
		await _userManager.UpdateAsync(newUser!);

		// هذه الطريقه في حاله انك بتحدث كم كبير من الريكورد لانها بتسلكت فقط الداتا اللي هتتحدث فبرفكتو بيرفومنس وكذلك في الحذف
		/*await _userManager.Users
			.Where(x => x.Id == userId)
			.ExecuteUpdateAsync(set => set
				.SetProperty(u => u.FirstName, request.FirstName)
				.SetProperty(u => u.LastName, request.LastName)
				);*/
		return new Successes("Profile updated successfully");
	}
	public async Task<OneOf<Successes, Errors>> ChangePasswordAsync(string userId, ChangePasswordRequest request, CancellationToken cancellationToken = default)
	{
		var user = await _userManager.FindByIdAsync(userId);
		var result = await _userManager.ChangePasswordAsync(user!, request.currentPassword, request.newPassword);//هتهندل currentReqPass ==  currentDbPass
		if (result.Succeeded)
			return new Successes("Password changed successfully");
		var error = result.Errors.First();
		return new Errors(error.Code, error.Description, StatusCodes.Status400BadRequest);
	}
}
