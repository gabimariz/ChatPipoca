using Domain.Entities;
using Infra.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data;

public class AppDbContext : DbContext
{
	public AppDbContext(DbContextOptions<AppDbContext> options)
		: base(options) {}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		new UserMap(modelBuilder.Entity<User>());
		new ProfileMap(modelBuilder.Entity<Profile>());
		new TokenMap(modelBuilder.Entity<Token>());
		new GlobalChatMap(modelBuilder.Entity<GlobalChat>());
	}

	public DbSet<User>? Users { get; set; }
	public DbSet<Profile>? Profiles { get; set; }
	public DbSet<Token>? Tokens { get; set; }

	public DbSet<GlobalChat>? GlobalChats { get; set; }
}
