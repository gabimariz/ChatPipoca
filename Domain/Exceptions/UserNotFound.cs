namespace Domain.Exceptions;

public class UserNotFound : Exception
{
	public UserNotFound()
		: base("User not found") {}
}
