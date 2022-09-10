using Domain.Entities;
using Domain.Interfaces;
using Domain.Utils;
using Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories;

public class TokensRepository : ITokensRepository
{
	private readonly AppDbContext _context;

	public TokensRepository(AppDbContext context)
	{
		_context = context;
	}

	public async Task<Token> Post(Guid id)
	{
		var token = new Token
		{
			Id = Guid.NewGuid(),
			Pass = Pass.Generate(BCrypt.Net.BCrypt.HashPassword(Guid.NewGuid().ToString())),
			UserId = id,
			ExpiresIn = DateTime.Now.AddMinutes(7)
		};

		await _context.Tokens!.AddAsync(token);

		return token;
	}

	public async Task<Token> GetByToken(string pass)
		=> await _context.Tokens!.FirstAsync(p => p.Pass!.Equals(pass));

	public async Task Destroy(Guid id)
		=> _context.Tokens!.Remove(await _context.Tokens!.FirstAsync(p => p.Id.Equals(id)));

	public async Task<bool> Commit() => await _context.SaveChangesAsync() > 0;

	public async Task Rollback() => await Task.CompletedTask;

	public async void Dispose() => await _context.DisposeAsync();
}
