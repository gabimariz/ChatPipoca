using Domain.Entities;
using Domain.Interfaces;
using Domain.Models;
using Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories;

public class GlobalChatsRepository : IGlobalChatsRepository
{
	private readonly AppDbContext _context;

	public GlobalChatsRepository(AppDbContext context)
	{
		_context = context;
	}

	public async Task<List<GlobalChat>> GetAll()
		=> await _context.GlobalChats!.Include(p => p.Profile).ToListAsync();

	public async Task Post(GlobalChatInput input)
		=> await _context.GlobalChats!.AddAsync(
			new GlobalChat
			{
				Id = Guid.NewGuid(),
				Message = input.Message,
				From = input.FromId,
				To = input.ToId,
				CreateAt = DateTime.Now,
				UpdateAt = DateTime.Now
			}
			);

	public async Task Destroy(DateTime time)
		=> _context.GlobalChats!.Remove(
				await _context.GlobalChats!.Where(p => p.CreateAt.AddHours(24) <= DateTime.Now).FirstAsync()
			);

	public async Task<bool> Commit() => await _context.SaveChangesAsync() > 0;

	public async Task Rollback() => await Task.CompletedTask;

	public async void Dispose() => await _context.DisposeAsync();
}
