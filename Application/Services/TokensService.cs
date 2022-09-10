using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;

public class TokensService : ITokensService
{
	private readonly ITokensRepository _repository;

	public TokensService(ITokensRepository repository)
	{
		_repository = repository;
	}

	public async Task<Token> Post(Guid id)
	{
		var token = await _repository.Post(id);

		if (!await _repository.Commit())
			throw new DbUpdateException();

		return token;
	}

	public async Task<Token> GetByToken(string pass)
	{
		var token = await _repository.GetByToken(pass);

		if (token is null)
			throw new NoContentException();
		if (token.ExpiresIn < DateTime.Now)
			throw new TimeoutException();

		return token;
	}

	public async Task Destroy(Guid id)
	{
		await _repository.Destroy(id);
		await _repository.Commit();
	}
}
