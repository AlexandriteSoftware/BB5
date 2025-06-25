using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace BB5.Components;

public partial class Textarea
{
    [Parameter]
    public string Value { get; set; } = "";

    [Parameter]
    public EventCallback<string> ValueChanged { get; set; }

    [Parameter]
    public ValidationState ValidationState { get; set; }

    [Parameter]
    public string Id { get; set; } = "";
    
    [Parameter]
    public int? Rows { get; set; }

    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? Attributes { get; set; }

    private string Classes { get; set; } = "";

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        var classList =
            new List<string>
            {
                "form-control"
            };

        switch (ValidationState)
        {
            case ValidationState.Valid:
                classList.Add("is-valid");
                break;
            case ValidationState.Invalid:
                classList.Add("is-invalid");
                break;
            case ValidationState.None:
            default:
                break;
        }

        Classes =
            string.Join(
                " ",
                classList);

        if (!string.IsNullOrEmpty(Id))
            Attributes["id"] = Id;
        
        if (Rows.HasValue)
            Attributes["rows"] = Rows.Value;
    }

    private async Task HandleInputAsync(
        ChangeEventArgs e)
    {
        await ValueChanged.InvokeAsync(e.Value?.ToString());
    }
}
