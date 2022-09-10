namespace Domain.Exceptions;

public class InvalidPasswordException : Exception
{
	public InvalidPasswordException()
		: base("Password don't match") {}
}
