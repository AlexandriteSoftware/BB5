using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;

namespace BB5;

public partial class Label
{
    [Parameter]
    public object? Content { get; set; }
    
    [Parameter]
    public RenderFragment? ChildContent { get; set; }
    
    [Parameter]
    public ComponentSize Size { get; set; }
    
    [Parameter]
    public string For { get; set; } = "";

    [Parameter]
    public string Class { get; set; } = "";

    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? Attributes { get; set; }

    private Dictionary<string, object?>? LabelAttributes { get; set; }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();
        
        var classes =
            new List<string>
            {
                "form-label"
            };

        if (!string.IsNullOrEmpty(Class))
        {
            classes.AddRange(
                Class.Split(
                    ' ',
                    StringSplitOptions.RemoveEmptyEntries));
        }
        
        switch (Size)
        {
            case ComponentSize.Small:
                classes.Add("small");
                break;
            case ComponentSize.Large:
                classes.Add("fs-5");
                break;
            case ComponentSize.Default:
            default:
                break;
        }

        LabelAttributes = [];

        if (!string.IsNullOrEmpty(For))
            LabelAttributes["for"] = For;

        if (classes.Count > 0)
        {
            LabelAttributes["class"] =
                string.Join(
                    " ",
                    classes);
        }

    }
}
