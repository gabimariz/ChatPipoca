using Domain.Exceptions;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers;

[ApiController]
[Route("v1/tokens")]
public class TokensController : ControllerBase
{
	private readonly ITokensService _tokens;
	private readonly IUsersService _users;

	public TokensController(
		ITokensService tokens,
		IUsersService users
	)
	{
		_tokens = tokens;
		_users = users;
	}

	/// <summary>
	///		Active your account
	/// </summary>
	/// <param name="pass"></param>
	/// <response code="200">Account activated successfully</response>
	/// <response code="422">Unable to activate your account</response>
	/// <response code="422">Activation time expired</response>
	[HttpGet("{pass}")]
	public async Task<ActionResult> GetByPass([FromRoute] string pass)
	{
		try
		{
			var token = await _tokens.GetByToken(pass);

			await _users.PutActive(token.UserId);

			await _tokens.Destroy(token.Id);

			return Ok("Account activated successfully");
		}
		catch (NoContentException)
		{
			return UnprocessableEntity("Unable to activate your account");
		}
		catch (TimeoutException)
		{
			return UnprocessableEntity("Activation time expired");
		}
	}
}
