using Domain.Entities;
using Domain.Entities.Enums;
using Domain.Interfaces;
using Domain.Models;
using Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories;

public class UsersRepository : IUsersRepository
{
	private readonly AppDbContext _context;

	public UsersRepository(AppDbContext context)
	{
		_context = context;
	}

	public async Task<List<User>> GetAll()
		=> await _context.Users!.Include(p => p.Profile).ToListAsync();

	public async Task<User> Post(UserInput input)
	{
		var user = new User
		{
			Id = Guid.NewGuid(),
			Email = input.Email,
			Password = BCrypt.Net.BCrypt.HashPassword(input.Password),
			Role = Roles.User,
			IsEnabled = false,
			CreateAt = DateTime.Now,
			UpdateAt = DateTime.Now
		};

		await _context.Users!.AddAsync(user);

		return user;
	}

	public async Task<User> GetById(Guid id)
		=> await _context.Users!.Include(p => p.Profile)
			.FirstOrDefaultAsync(p => p.Id.Equals(id)) ?? null!;

	public async Task<User> GetByEmail(string email)
		=> await _context.Users!.Include(p => p.Profile)
			.FirstAsync(p => p.Email!.Equals(email));

	public async Task PutActive(Guid id)
	{
		var user = await _context.Users!
			.FirstAsync(p => p.Id.Equals(id));

		user.IsEnabled = true;
		_context.Update(user);
	}

	public async Task Destroy(Guid id)
		=> _context.Users!.Remove(
			await _context.Users!.Where(p => p.Id.Equals(id))
				.Include(p => p.Profile).FirstAsync());

	public async Task<bool> Commit() => await _context.SaveChangesAsync() > 0;

	public async Task Rollback() => await Task.CompletedTask;

	public async void Dispose() => await _context.DisposeAsync();
}
