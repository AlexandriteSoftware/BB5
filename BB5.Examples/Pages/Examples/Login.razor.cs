using System.ComponentModel;

namespace BB5.Examples.Pages.Examples;

public class LoginModel
{
    public string User { get; set; } = "";
    
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