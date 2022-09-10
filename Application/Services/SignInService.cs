using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Models;

namespace Application.Services;

public class SignInService : ISignInService
{
	private readonly IUsersRepository _repository;

	public SignInService(IUsersRepository repository)
	{
		_repository = repository;
	}

	public async Task<User> GetByEmail(SignInInput input)
	{
		var user = await _repository.GetByEmail(input.Email!);

		if (!user.IsEnabled)
			throw new UserNotActivatedException();
		if (user is null)
			throw new UserNotFound();

		if (BCrypt.Net.BCrypt.Verify(input.Password, user.Password))
			return user;

		throw new InvalidPasswordException();
	}
}
