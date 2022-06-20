using System.ComponentModel.DataAnnotations;
namespace UnityAnalyze.Shared.Auth;

public class RegisterRequest
{
	[Required]
	public string Email { get; set; }
	
	[Required]
	public string CompanyName { get; set; }
	
	[Required]
	public string Password { get; set; }
	
	[Required]
	[Compare(nameof(Password), ErrorMessage = "Passwords do not match!")]
	public string PasswordConfirm { get; set; }
}
