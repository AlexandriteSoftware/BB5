using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace BB5;

public enum ButtonVariant
{
    Default,
    Primary,
    Secondary,
    Success,
    Danger,
    Warning,
    Info,
    Light,
    Dark,
    Link
}

public partial class Button
{
    [Parameter]
    public ButtonVariant Variant { get; set; }

    [Parameter]
    public object? Content { get; set; }
    
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    [Parameter]
    public EventCallback OnClick { get; set; }
    
    [Parameter]
    public bool Outline { get; set; }
    
    [Parameter]
    public ComponentSize Size { get; set; }
    
    [Parameter]
    public bool Disabled { get; set; }

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
                "btn"
            };
        
        var btnClass =
            Outline
                ? "btn-outline"
                : "btn";

        switch (Variant)
        {
            case ButtonVariant.Primary:
                classes.Add(btnClass + "-primary");
                break;
            case ButtonVariant.Secondary:
                classes.Add(btnClass + "-secondary");
                break;
            case ButtonVariant.Success:
                classes.Add(btnClass + "-success");
                break;
            case ButtonVariant.Danger:
                classes.Add(btnClass + "-danger");
                break;
            case ButtonVariant.Warning:
                classes.Add(btnClass + "-warning");
                break;
            case ButtonVariant.Info:
                classes.Add(btnClass + "-info");
                break;
            case ButtonVariant.Light:
                classes.Add(btnClass + "-light");
                break;
            case ButtonVariant.Dark:
                classes.Add(btnClass + "-dark");
                break;
            case ButtonVariant.Link:
                classes.Add("btn-link");
                break;
            case ButtonVariant.Default:
            default:
                break;
        }
        
        switch (Size)
        {
            case ComponentSize.Small:
                classes.Add("btn-sm");
                break;
            case ComponentSize.Large:
                classes.Add("btn-lg");
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

        Attributes ??= [];
        
        Attributes["class"] =
            string.Join(
                " ",
                classes);

        if (Disabled)
            Attributes.Add("disabled", "1");
    }

    private async Task HandleClickAsync()
    {
        await OnClick.InvokeAsync();
    }
}
