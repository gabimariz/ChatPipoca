using Domain.Entities.Enums;

namespace Domain.Entities;

public class User
{
	public Guid Id { get; set; }

	public string? Email { get; set; }

	public string? Password { get; set; }

	public virtual Profile? Profile { get; set; }

	public Roles Role { get; set; }

	public bool IsEnabled { get; set; }

	public DateTime CreateAt { get; set; }

	public DateTime UpdateAt { get; set; }
}
