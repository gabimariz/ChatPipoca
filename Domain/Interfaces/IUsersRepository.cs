using Domain.Entities;
using Domain.Models;

namespace Domain.Interfaces;

public interface IUsersRepository : IUnitOfWork
{
	Task<List<User>> GetAll();

	Task<User> Post(UserInput input);

	Task<User> GetById(Guid id);

	Task<User> GetByEmail(string email);

	Task PutActive(Guid id);

	Task Destroy(Guid id);
}
