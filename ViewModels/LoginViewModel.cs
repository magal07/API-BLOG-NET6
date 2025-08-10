using System.ComponentModel.DataAnnotations;

namespace Blog.ViewModels;

public class LoginViewModel
{
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Email is invalid")]
    public string Email { get; set; }  
    
    [Required(ErrorMessage = "Enter your password")]
    public string Password { get; set; }  
}