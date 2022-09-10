using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;

public class UsersService : IUsersService
{
	private readonly IUsersRepository _repository;

	public UsersService(IUsersRepository repository)
	{
		_repository = repository;
	}

	public async Task<List<User>> GetAll()
	{
		var users = await _repository.GetAll();

		if (users.Count.Equals(0))
			throw new NoContentException();

		return users;
	}

	public async Task<User> Post(UserInput input)
	{
		var user = await _repository.Post(input);

		if (!await _repository.Commit())
			throw new DbUpdateException();

		return user;
	}

	public async Task<User> GetById(Guid id)
	{
		var user = await _repository.GetById(id);

		if (user is null)
			throw new UserNotFound();

		if (user.Profile is null)
			throw new IncompleteProfileException();

		return user;
	}

	public async Task PutActive(Guid id)
	{
		await _repository.PutActive(id);

		if (!await _repository.Commit())
			throw new DbUpdateException();
	}

	public async Task Destroy(Guid id)
	{
		await _repository.Destroy(id);
		await _repository.Commit();
	}
}
