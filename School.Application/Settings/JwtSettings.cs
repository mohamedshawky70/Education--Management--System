using System.ComponentModel.DataAnnotations;
namespace School.Application.Settings;
public class JwtSettings
{
	[Required]
	public string Key { get; set; } = null!;
	[Required]
	public string Issuer { get; set; } = null!;
	[Required]
	public string Audience { get; set; } = null!;
	[Range(1, int.MaxValue)]
	public int ExpiryMinutes { get; set; }
}
