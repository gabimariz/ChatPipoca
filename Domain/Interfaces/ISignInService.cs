using Domain.Entities;
using Domain.Models;

namespace Domain.Interfaces;

public interface ISignInService
{
	Task<User> GetByEmail(SignInInput input);
}
