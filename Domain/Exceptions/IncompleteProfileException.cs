namespace Domain.Exceptions;

public class IncompleteProfileException : Exception
{
	public IncompleteProfileException()
		: base("Incomplete user profile") {}
}
