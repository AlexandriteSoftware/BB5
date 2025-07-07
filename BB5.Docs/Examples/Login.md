# Login

Requirements:

- Login is a simple form, with two fields: email and password.
- Both fields are required.
- Email has a custom validator attached.
- Form is submitted either with the Enter key or by clicking the Login button.
- When a user/password pair is unknown, the form displays an error message.

Form model:

```csharp
public class LoginModel
{
    [Required] 
    [AllowedEmailDomains("local")]
    public string Email { get; set; } = string.Empty;

    [Required]
    [PasswordPropertyText]
    public string Password { get; set; } = string.Empty;
}
```

Custom validator:

```csharp
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
```

Form:

```razor
<Form Object="@Model"
      Submitted="HandleSubmitAsync"
      SubmitContent="@("Login")" />
```

Code-behind:

```csharp
private LoginModel Model { get; set; } = new();

private Task HandleSubmitAsync(
    FormActionContext context)
{
    var model = (LoginModel)context.Object!;
    
    // simulate a login check
    
    if (model.Email != "user@local"
        && model.Password != "secret")
    {
        context.Errors.Add(
            new ValidationResult(
                "Invalid email or password."));

        Message = "";
        return Task.CompletedTask;
    }

    Message = "Login successful."

    return Task.CompletedTask;
}
```
