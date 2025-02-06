using System.ComponentModel.DataAnnotations;

namespace BlogApi.ViewModels;

public class LoginViewModel
{
    [Required(ErrorMessage = "E-mail is required")]
    [EmailAddress(ErrorMessage = "Invalid Email Address")]
    public string Email { get; set; }
    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }
}