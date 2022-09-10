using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Application.Controllers;

[ApiController]
[Route("v1/profiles")]
[Authorize]
public class ProfilesController : ControllerBase
{
	private readonly IProfilesService _profiles;

	public ProfilesController(IProfilesService profiles)
	{
		_profiles = profiles;
	}

	/// <summary>
	///		Create profile
	/// </summary>
	/// <param name="input"></param>
	/// <returns></returns>
	[HttpPost]
	public async Task<ActionResult> Post([FromBody] ProfileInput input)
	{
		try
		{
			await _profiles.Post(input);

			return Ok("Profile Created successfully");
		}
		catch (DbUpdateException)
		{
			return UnprocessableEntity("Unable to create your profile");
		}
	}
}
