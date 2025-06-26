using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace BB5;

public partial class Form
{
    [Parameter]
    public object? Item { get; set; }
    
    [Parameter]
    public EventCallback<object?> OnSubmit { get; set; }

    private async Task HandleSubmit()
    {
        await OnSubmit.InvokeAsync();
    }

    private string GetValidationFeedback(
        string id)
    {
        return "OK";
    }

    private ValidationState GetValidationState(
        string id)
    {
        return ValidationState.Valid;
    }
}