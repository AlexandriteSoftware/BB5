using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace BB5.Components;

public enum AlertColor
{
    Default,
    Primary,
    Secondary,
    Success,
    Danger,
    Warning,
    Info,
    Light,
    Dark
}

public partial class Alert
{
    [Parameter]
    public AlertColor Color { get; set; }

    [Parameter]
    public object? Content { get; set; }
    
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    [Parameter]
    public bool Dismissible { get; set; }
    
    [Parameter]
    public EventCallback OnDismiss { get; set; }

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
                "alert"
            };

        if (Dismissible)
            classes.Add("alert-dismissible");

        switch (Color)
        {
            case AlertColor.Primary:
                classes.Add("alert-primary");
                break;
            case AlertColor.Secondary:
                classes.Add("alert-secondary");
                break;
            case AlertColor.Success:
                classes.Add("alert-success");
                break;
            case AlertColor.Danger:
                classes.Add("alert-danger");
                break;
            case AlertColor.Warning:
                classes.Add("alert-warning");
                break;
            case AlertColor.Info:
                classes.Add("alert-info");
                break;
            case AlertColor.Light:
                classes.Add("alert-light");
                break;
            case AlertColor.Dark:
                classes.Add("alert-dark");
                break;
            case AlertColor.Default:
            default:
                break;
        }

        if (!string.IsNullOrEmpty(Class))
        {
            classes.AddRange(
                Class.Split(
                    ',',
                    StringSplitOptions.RemoveEmptyEntries));
        }

        Attributes ??= [];

        Attributes["role"] = "alert";

        Attributes["class"] =
            string.Join(
                " ",
                classes);
    }

    private async Task DismissAsync()
    {
        await OnDismiss.InvokeAsync();
    }
}
