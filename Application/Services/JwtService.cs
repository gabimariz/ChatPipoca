using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Utils;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services;

public class JwtService : IJwtService
{
	public string Generate(User user)
	{
		var tokenHandler = new JwtSecurityTokenHandler();
		var key = Encoding.ASCII.GetBytes(Secret.Key);

		var tokenDescriptor = new SecurityTokenDescriptor
		{
			Subject = new ClaimsIdentity(new[]
			{
				new Claim(ClaimTypes.Email, user.Email!),
				new Claim(ClaimTypes.Role, user.Role.ToString())
			}),
			Expires = DateTime.Now.AddHours(6),
			SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
				SecurityAlgorithms.HmacSha256Signature)
		};

		var token = tokenHandler.CreateToken(tokenDescriptor);

		return tokenHandler.WriteToken(token);
	}
}
