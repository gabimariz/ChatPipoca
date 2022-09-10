using Domain.Models;

namespace Domain.Interfaces;

public interface IProfilesService
{
	Task Post(ProfileInput input);
}
