using System.ComponentModel.DataAnnotations;

namespace BlogApi.ViewModels;

// essa classe serve para trabalhar com registro do usuário
public class RegisterViewModel
{
    [Required(ErrorMessage = "Username is required")]
    public string Name { get; set; }
    
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string Email { get; set; }
}