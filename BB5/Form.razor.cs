using System;
using System.Collections.Generic;
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
    
    [Parameter]
    public string Class { get; set; } = "";

    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? Attributes { get; set; }
    
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        var classes =
            new List<string>();

        if (!string.IsNullOrEmpty(Class))
        {
            classes.AddRange(
                Class.Split(
                    ' ',
                    StringSplitOptions.RemoveEmptyEntries));
        }

        Attributes ??= [];

        Attributes["class"] =
            string.Join(
                " ",
                classes);
    }

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