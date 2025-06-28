using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BB5.Examples.Pages.Examples;

public class LoginModel
{
    [Required]
    [TrimText]
    public string User { get; set; } = "";
    
    [Required]
    [PasswordPropertyText]
    public string Password { get; set; } = "";
}

public partial class Login
{
    private LoginModel LoginModel { get; set; } = new();
    private string Message { get; set; } = "";

    private void HandleLogin()
    {
        Message = "Login successful for user: " + LoginModel.User;
    }
}