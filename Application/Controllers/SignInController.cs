using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers;

[ApiController]
[Route("v1/signin")]
public class SignInController : ControllerBase
{
	private readonly ISignInService _signIn;
	private readonly IJwtService _jwt;

	public SignInController(
		ISignInService signIn,
		IJwtService jwt
	)
	{
		_signIn = signIn;
		_jwt = jwt;
	}

	/// <summary>
	///		Login user
	/// </summary>
	/// <param name="input"></param>
	/// <returns>JWT token</returns>
	[HttpPost]
	public async Task<ActionResult> Post([FromBody] SignInInput input)
	{
		var user = await _signIn.GetByEmail(input);

		try
		{

			var jwt = _jwt.Generate(user);

			return Ok(new
			{
				token = jwt
			});
		}
		catch (UserNotFound)
		{
			return NotFound("User not found");
		}
		catch (InvalidPasswordException)
		{
			return BadRequest("Passwords not match");
		}
		catch (UserNotActivatedException)
		{
			return BadRequest("User not activated");
		}
	}
}
