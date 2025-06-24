using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace BB5.Components;

public partial class Alert
{
    [Parameter]
    public AttentionState AttentionState { get; set; }

    [Parameter]
    public string Content { get; set; } = "";
    
    [Parameter]
    public ContentType ContentType { get; set; } = ContentType.Text;

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

        if (!string.IsNullOrEmpty(Class))
        {
            classes.AddRange(
                Class.Split(
                    ',',
                    StringSplitOptions.RemoveEmptyEntries));
        }

        if (Dismissible)
            classes.Add("alert-dismissible");

        switch (AttentionState)
        {
            case AttentionState.Primary:
                classes.Add("alert-primary");
                break;
            case AttentionState.Secondary:
                classes.Add("alert-secondary");
                break;
            case AttentionState.Success:
                classes.Add("alert-success");
                break;
            case AttentionState.Danger:
                classes.Add("alert-danger");
                break;
            case AttentionState.Warning:
                classes.Add("alert-warning");
                break;
            case AttentionState.Info:
                classes.Add("alert-info");
                break;
            case AttentionState.Light:
                classes.Add("alert-light");
                break;
            case AttentionState.Dark:
                classes.Add("alert-dark");
                break;
            case AttentionState.None:
            default:
                break;
        }

        Attributes ??= [];

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
