using Domain.Entities;
using Domain.Models;

namespace Domain.Interfaces;

public interface IGlobalChatsRepository : IUnitOfWork
{
	Task<List<GlobalChat>> GetAll();

	Task Post(GlobalChatInput input);

	Task Destroy(DateTime time);
}
