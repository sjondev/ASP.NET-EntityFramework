using System.ComponentModel.DataAnnotations;

namespace BlogApi.ViewModels;

public class RegisterViewModel
{
    [Required(ErrorMessage = "Username is required")]
    public string Name { get; set; }
    
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string Email { get; set; }
}