using Domain.Entities;

namespace Domain.Interfaces;

public interface IJwtService
{
	string Generate(User user);
}
