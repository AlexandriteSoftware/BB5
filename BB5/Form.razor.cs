using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace BB5;

public partial class Form
{
    [Parameter]
    public object? Object { get; set; }
    
    [Parameter]
    public EventCallback OnModified { get; set; }

    [Parameter]
    public EventCallback<object?> OnSubmit { get; set; }
    
    [Parameter]
    public RenderFragment? Controls { get; set; }

    [Parameter]
    public RenderFragment? Actions { get; set; }
    
    private async Task HandleSubmit()
    {
        await OnSubmit.InvokeAsync();
    }

    private string GetValidationFeedback(
        string id)
    {
        return "";
    }

    private ValidationState GetValidationState(
        string id)
    {
        return ValidationState.None;
    }
}