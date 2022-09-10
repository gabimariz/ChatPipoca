using System.ComponentModel.DataAnnotations;

namespace Domain.Models;

public class UserInput
{
	[Required(ErrorMessage = "Email required")]
	[EmailAddress(ErrorMessage = "Invalid email")]
	public string? Email { get; set; }

	[Required(ErrorMessage = "Password required")]
	public string? Password { get; set; }
}
