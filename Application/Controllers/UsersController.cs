using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Models;
using Domain.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Application.Controllers;

[ApiController]
[Route("v1/users")]
public class UsersController : ControllerBase
{
	private readonly IUsersService _users;
	private readonly ITokensService _tokens;

	public UsersController(
		IUsersService users,
		ITokensService tokens
	)
	{
		_users = users;
		_tokens = tokens;
	}

	/// <summary>
	///		List all users
	/// </summary>
	/// <response code="200">All users</response>
	/// <response code="204">No content</response>
	[Authorize(Roles = "Admin")]
	[HttpGet]
	public async Task<ActionResult<List<User>>> GetAll()
	{
		try
		{
			return Ok(await _users.GetAll());
		}
		catch (NoContentException)
		{
			return NoContent();
		}
	}

	/// <summary>
	///		Create new user
	/// </summary>
	/// <param name="input"></param>
	/// <response code="200">User created successfully</response>
	/// <response code="422">There is already and account registered</response>
	[HttpPost]
	public async Task<ActionResult> Post([FromBody] UserInput input)
	{
		try
		{
			var user = await _users.Post(input);
			var token = await _tokens.Post(user.Id);

			Email.Send(
				user.Email!,
				"Active account",
				$"<a href=\"{Secret.ApiHost}/tokens/{token.Pass}\">Click here</a> to activate your account"
				);

			return Ok("User created successfully");
		}
		catch (DbUpdateException)
		{
			return UnprocessableEntity("There is already and account registered");
		}
	}

	/// <summary>
	///		Get user by id
	/// </summary>
	/// <param name="id"></param>
	/// <response code="200">User</response>
	/// <response code="401">Fill in your profile to access</response>
	/// <response code="404">User not found</response>
	[Authorize]
	[HttpGet("{id:guid}")]
	public async Task<ActionResult<User>> GetById([FromRoute] Guid id)
	{
		try
		{
			return Ok(await _users.GetById(id));
		}
		catch (IncompleteProfileException)
		{
			return Unauthorized("Fill in your profile to access");
		}
		catch (UserNotFound)
		{
			return NotFound("User not found");
		}
	}

	/// <summary>
	///		Delete user by id
	/// </summary>
	/// <param name="id"></param>
	/// <response code="200">User deleted</response>
	/// <response code="404">User not found</response>
	[HttpDelete("{id:guid}")]
	public async Task<ActionResult> Destroy([FromRoute] Guid id)
	{
		try
		{
			await _users.Destroy(id);

			return Ok("User deleted");
		}
		catch (InvalidOperationException)
		{
			return NotFound("User not found");
		}
	}
}
