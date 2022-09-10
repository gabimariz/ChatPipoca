using Domain.Entities;
using Infra.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data;

public class AppDbContext : DbContext
{
	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseMySql(
			"server=localhost;username=gabriel;password=root;database=cp_data",
			new MariaDbServerVersion(new Version(10, 8, 3)));
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		new UserMap(modelBuilder.Entity<User>());
		new ProfileMap(modelBuilder.Entity<Profile>());
		new TokenMap(modelBuilder.Entity<Token>());
	}

	public DbSet<User>? Users { get; set; }
	public DbSet<Profile>? Profiles { get; set; }
	public DbSet<Token>? Tokens { get; set; }
}
