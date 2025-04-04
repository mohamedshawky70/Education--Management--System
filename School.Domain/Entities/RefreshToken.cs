using Microsoft.EntityFrameworkCore;

namespace School.Domain.Entities;

[Owned]
public class RefreshToken
{
	public string Token { get; set; } = string.Empty;
	public DateTime CreatedIn { get; set; } = DateTime.UtcNow;
	public DateTime ExpiresIn { get; set; }
	public DateTime? RevokedIn { get; set; }
	public bool IsExpire => DateTime.UtcNow > ExpiresIn;
	public bool IsActive => RevokedIn is null && !IsExpire;
}
