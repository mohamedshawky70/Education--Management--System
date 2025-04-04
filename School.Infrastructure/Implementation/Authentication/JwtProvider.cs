using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using School.Application.Interfaces.IAuthentication;
using School.Application.Settings;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace School.Infrastructure.Implementation.Authentication
{
	public class JwtProvider : IJwtProvider
	{
		private readonly JwtSettings _jwtSettings;

		public JwtProvider(IOptions<JwtSettings> jwtSettings)
		{
			_jwtSettings = jwtSettings.Value;
		}

		//tuple to return 2 value
		public (string taken, int expireIn) GenerateTaken(ApplicationUser user, IEnumerable<string> roles)
		{
			//Generate claims that add in taken
			Claim[] claims = new Claim[]
			{
			new Claim(JwtRegisteredClaimNames.Sub, user.Id),
			new Claim(JwtRegisteredClaimNames.Email, user.Email!),
			new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
			new Claim(JwtRegisteredClaimNames.FamilyName,user.LastName),
			new Claim(nameof(roles),JsonSerializer.Serialize(roles),JsonClaimValueTypes.JsonArray),
			new Claim(JwtRegisteredClaimNames.Jti, Guid.CreateVersion7().ToString()),
			};
			//Generate Key to encrypt and decrypt taken
			var SymmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
			//المسئول عن تشفير الايميل واللباس فبديله المفتاح اللي هيشفر بيه والاجورزم نوع التشفير
			var signingCredentials = new SigningCredentials(SymmetricSecurityKey, SecurityAlgorithms.HmacSha256);

			var expiresIn = _jwtSettings.ExpiryMinutes;
			var taken = new JwtSecurityToken(
				issuer: _jwtSettings.Issuer,//مين اللي عمل التوكن
				audience: _jwtSettings.Audience,// لمين التوكن دي
				claims: claims, //معلومات خاصه باليوزر صاحب هذه التوكن
				expires: DateTime.UtcNow.AddMinutes(expiresIn),
				signingCredentials: signingCredentials
			);
			return (taken: new JwtSecurityTokenHandler().WriteToken(taken), expireIn: expiresIn);
		}
		//Refresh Taken
		public string? ValidateTaken(string taken)
		{
			var takenHandler = new JwtSecurityTokenHandler();
			try
			{
				//To decrypt taken
				takenHandler.ValidateToken(taken, new TokenValidationParameters
				{
					ValidateIssuer = false, //متعملش validate
					ValidateAudience = false,
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key)), //المفتاح المسئول عن فك التشفير
					ClockSkew = TimeSpan.Zero //الديفولت لو لقي التوكن اكسباير استني خمس دجايج انا مش عايز كده
				}, out SecurityToken securityToken);//المكان اللي هتتخزن في التوكن بعد فك التشفير 
				var JwtTaken = (JwtSecurityToken)securityToken; //JwtSecurityToken بص فوق هتلاقيه يحتوي علي كل الكليمز
				return JwtTaken.Claims.First(x => x.Type == JwtRegisteredClaimNames.Sub).Value;
			}
			catch (Exception)
			{
				return null;//التوكن بعد فك التشفير مش مظبوطه 
			}
		}
	}
}

