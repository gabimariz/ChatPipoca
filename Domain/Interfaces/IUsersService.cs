using Domain.Entities;
using Domain.Models;

namespace Domain.Interfaces;

public interface IUsersService
{
	Task<List<User>> GetAll();

	Task<User> Post(UserInput input);

	Task<User> GetById(Guid id);

	Task PutActive(Guid id);

	Task Destroy(Guid id);
}
