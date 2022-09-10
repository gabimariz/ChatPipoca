using System.ComponentModel.DataAnnotations;
using Domain.Entities.Enums;

namespace Domain.Models;

public class ProfileInput
{
	[Required(ErrorMessage = "Firstname require")]
	[MaxLength(40, ErrorMessage = "40 character limit")]
	public string? Firstname { get; set; }

	[Required(ErrorMessage = "Lastname required")]
	public string? Lastname { get; set; }

	public string? PhoneNumber { get; set; }

	public Gender Gender { get; set; }

	public Relationship Relationship { get; set; }

	[Required(ErrorMessage = "User id required")]
	public Guid UserId { get; set; }
}
