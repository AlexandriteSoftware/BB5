using System.Collections.Generic;
using Microsoft.AspNetCore.Components;

namespace BB5;

public partial class Badge
{
    [Parameter]
    public TextBackgroundColor Color { get; set; }

    [Parameter]
    public object? Content { get; set; }
    
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

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
                "badge"
            };
        
        switch (Color)
        {
            case TextBackgroundColor.Primary:
                classes.Add("text-bg-primary");
                break;
            case TextBackgroundColor.Secondary:
                classes.Add("text-bg-secondary");
                break;
            case TextBackgroundColor.Success:
                classes.Add("text-bg-success");
                break;
            case TextBackgroundColor.Danger:
                classes.Add("text-bg-danger");
                break;
            case TextBackgroundColor.Warning:
                classes.Add("text-bg-warning");
                break;
            case TextBackgroundColor.Info:
                classes.Add("text-bg-info");
                break;
            case TextBackgroundColor.Light:
                classes.Add("text-bg-light");
                break;
            case TextBackgroundColor.Dark:
                classes.Add("text-bg-dark");
                break;
            case TextBackgroundColor.None:
            default:
                break;
        }

        Attributes ??= [];

        Attributes["class"] =
            string.Join(
                " ",
                classes);
    }
}
