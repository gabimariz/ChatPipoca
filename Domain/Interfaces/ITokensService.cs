using Domain.Entities;

namespace Domain.Interfaces;

public interface ITokensService
{
	Task<Token> Post(Guid id);

	Task<Token> GetByToken(string pass);

	Task Destroy(Guid id);
}
