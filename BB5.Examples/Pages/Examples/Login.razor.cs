using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BB5.Examples.Pages.Examples;

public class LoginModel
{
    [Required] 
    [AllowedEmailDomains("local")]
    public string Email { get; set; } = string.Empty;

    [Required]
    [PasswordPropertyText]
    public string Password { get; set; } = string.Empty;
}

public class AllowedEmailDomainsAttribute(
        params string[] allowedDomains)
    : ValidationAttribute
{
    protected override ValidationResult? IsValid(
        object? value,
        ValidationContext validationContext)
    {
        if (value is string email
            && !string.IsNullOrEmpty(email))
        {
            var domain =
                email
                    .Split('@')
                    .LastOrDefault();

            if (domain != null
                && !allowedDomains.Contains(
                    domain,
                    StringComparer.OrdinalIgnoreCase))
            {
                return new ValidationResult(
                    $"Email domain '{domain}' is not allowed.");
            }
        }

        return ValidationResult.Success;
    }
}

public partial class Login
{
    private LoginModel Model { get; set; } = new();
    private string Message { get; set; } = "";

    private Task HandleSubmitAsync(
        FormActionContext context)
    {
        var model = (LoginModel)context.Object!;

        if (model.Email != "user@local"
            || model.Password != "secret")
        {
            context.Errors.Add(
                new ValidationResult(
                    "Invalid email or password."));

            Message = "";
            return Task.CompletedTask;
        }

        Message = "Login successful.";

        return Task.CompletedTask;
    }
}