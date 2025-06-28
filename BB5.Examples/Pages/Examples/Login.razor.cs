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

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        Login2Model = new LoginModel();
        Login2 = FormControlModel.From(Login2Model);
    }

    private LoginModel? Login2Model { get; set; }
    private IList<FormControlModel>? Login2 { get; set; }
    
    private void HandleLogin2()
    {
        Message = "Login successful for user: " + Login2Model!.User;
    }
}