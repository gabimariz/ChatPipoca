namespace Domain.Exceptions;

public class NoContentException : Exception
{
	public NoContentException()
		: base("No content") {}
}
