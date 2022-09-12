namespace Domain.Entities;

public class GlobalChat
{
	public Guid Id { get; set; }

	public string? Message { get; set; }

	public Guid From { get; set; }

	public virtual Profile? Profile { get; set; }

	public Guid? To { get; set; }

	public DateTime CreateAt { get; set; }

	public DateTime UpdateAt { get; set; }
}
