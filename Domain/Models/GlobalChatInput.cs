using System.ComponentModel.DataAnnotations;

namespace Domain.Models;

public class GlobalChatInput
{
	[Required(ErrorMessage = "Message required")]
	[MaxLength(160, ErrorMessage = "160 is the maximum number of characters allowed.")]
	public string? Message { get; set; }

	[Required(ErrorMessage = "User id required")]
	public Guid UserId { get; set; }

	public Guid FromId { get; set; }

	public Guid ToId { get; set; }
}
