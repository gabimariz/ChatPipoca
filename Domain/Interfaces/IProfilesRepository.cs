using Domain.Models;

namespace Domain.Interfaces;

public interface IProfilesRepository : IUnitOfWork
{
	Task Post(ProfileInput input);
}
