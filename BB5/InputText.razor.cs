using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace BB5;

public partial class InputText
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
    public ComponentSize Size { get; set; }

    [Parameter]
    public bool ReadOnly { get; set; }
    
    [Parameter]
    public bool Disabled { get; set; }
    
    [Parameter]
    public string? Placeholder { get; set; }

    [Parameter]
    public string Class { get; set; } = "";
    
    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? Attributes { get; set; }
    
    private Dictionary<string, object?>? InputAttributes { get; set; }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        var classes =
            new List<string>
            {
                "form-control"
            };

        switch (ValidationState)
        {
            case ValidationState.Valid:
                classes.Add("is-valid");
                break;
            case ValidationState.Invalid:
                classes.Add("is-invalid");
                break;
            case ValidationState.None:
            default:
                break;
        }

        switch (Size)
        {
            case ComponentSize.Small:
                classes.Add("form-control-sm");
                break;
            case ComponentSize.Large:
                classes.Add("form-control-lg");
                break;
            case ComponentSize.Default:
            default:
                break;
        }

        if (!string.IsNullOrEmpty(Class))
        {
            classes.AddRange(
                Class.Split(
                    ' ',
                    StringSplitOptions.RemoveEmptyEntries));
        }

        InputAttributes = [];

        if (!string.IsNullOrEmpty(Id))
            InputAttributes["id"] = Id;
        
        if (ReadOnly)
            InputAttributes["readonly"] = "readonly";
        
        if (!string.IsNullOrEmpty(Placeholder))
            InputAttributes["placeholder"] = Placeholder;

        if (classes.Count > 0)
        {
            InputAttributes["class"] =
                string.Join(
                    " ",
                    classes);
        }
    }

    private async Task HandleInputAsync(
        ChangeEventArgs e)
    {
        await ValueChanged.InvokeAsync(e.Value?.ToString());
    }
}
