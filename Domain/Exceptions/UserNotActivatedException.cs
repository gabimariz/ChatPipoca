namespace Domain.Exceptions;

public class UserNotActivatedException : Exception
{
	public UserNotActivatedException()
		: base("User not activated") {}
}
