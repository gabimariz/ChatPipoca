using Domain.Entities.Enums;

namespace Domain.Entities;

public class Profile
{
	public Guid Id { get; set; }

	public string? Firstname { get; set; }

	public string? Lastname { get; set; }

	public string? PhoneNumber { get; set; }

	public Gender Gender { get; set; }

	public Relationship Relationship { get; set; }

	public Guid UserId { get; set; }

	public virtual User? User { get; set; }

	public virtual GlobalChat? GlobalChat { get; set; }

	public DateTime CreateAt { get; set; }

	public DateTime UpdateAt { get; set; }
}
