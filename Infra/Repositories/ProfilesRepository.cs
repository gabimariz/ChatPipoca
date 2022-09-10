using Domain.Entities;
using Domain.Interfaces;
using Domain.Models;
using Infra.Data;

namespace Infra.Repositories;

public class ProfilesRepository : IProfilesRepository
{
	private readonly AppDbContext _context;

	public ProfilesRepository(AppDbContext context)
	{
		_context = context;
	}

	public async Task Post(ProfileInput input)
		=> await _context.Profiles!.AddAsync(new Profile
		{
			Id = Guid.NewGuid(),
			Firstname = input.Firstname,
			Lastname = input.Lastname,
			PhoneNumber = input.PhoneNumber,
			Gender = input.Gender,
			Relationship = input.Relationship,
			UserId = input.UserId,
			CreateAt = DateTime.Now,
			UpdateAt = DateTime.Now
		});

	public async Task<bool> Commit() => await _context.SaveChangesAsync() > 0;

	public async Task Rollback() => await Task.CompletedTask;

	public async void Dispose() => await _context.DisposeAsync();
}
