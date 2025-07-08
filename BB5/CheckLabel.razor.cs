using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;

namespace BB5;

public partial class CheckLabel
{
    [Parameter]
    public object? Content { get; set; }
    
    [Parameter]
    public RenderFragment? ChildContent { get; set; }
    
    [Parameter]
    public string For { get; set; } = "";

    [Parameter]
    public string Class { get; set; } = "";

    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? Attributes { get; set; }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();
        
        var classes =
            new List<string>
            {
                "form-check-label"
            };

        if (!string.IsNullOrEmpty(Class))
        {
            classes.AddRange(
                Class.Split(
                    ' ',
                    StringSplitOptions.RemoveEmptyEntries));
        }

        Attributes ??= [];
        
        if (!string.IsNullOrEmpty(For))
            Attributes["for"] = For;

        Attributes["class"] =
            string.Join(
                " ",
                classes);
    }
}
