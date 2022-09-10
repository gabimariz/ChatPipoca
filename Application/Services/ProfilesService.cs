using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;

public class ProfilesService : IProfilesService
{
	private readonly IProfilesRepository _repository;

	public ProfilesService(IProfilesRepository repository)
	{
		_repository = repository;
	}

	public async Task Post(ProfileInput input)
	{
		await _repository.Post(input);

		if (!await _repository.Commit())
			throw new DbUpdateException();
	}
}
