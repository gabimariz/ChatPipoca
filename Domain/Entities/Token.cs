namespace Domain.Entities;

public class Token
{
	public Guid Id { get; set; }

	public string? Pass { get; set; }

	public Guid UserId { get; set; }

	public DateTime ExpiresIn { get; set; }
}
