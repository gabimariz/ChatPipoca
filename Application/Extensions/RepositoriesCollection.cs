using Domain.Interfaces;
using Infra.Repositories;

namespace Application.Extensions;

public static class RepositoriesCollection
{
	public static IServiceCollection AddRepositories(this IServiceCollection services)
	{
		services.AddTransient<IUsersRepository, UsersRepository>();
		services.AddTransient<IProfilesRepository, ProfilesRepository>();
		services.AddTransient<ITokensRepository, TokensRepository>();

		return services;
	}
}
