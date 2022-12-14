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
	/// <response code="200">JWT token</response>
	/// <response code="404">User not found</response>
	/// <response code="400">Passwords not match</response>
	/// <response code="400">User not activated</response>
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
