using Application.Services;
using Domain.Interfaces;

namespace Application.Extensions;

public static class ServicesCollection
{
	public static IServiceCollection AddServices(this IServiceCollection services)
	{
		services.AddScoped<IUsersService, UsersService>();
		services.AddScoped<IProfilesService, ProfilesService>();
		services.AddScoped<ITokensService, TokensService>();
		services.AddScoped<ISignInService, SignInService>();
		services.AddScoped<IJwtService, JwtService>();

		return services;
	}
}
