﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace BB5;

public partial class InputPassword
{
    [Parameter]
    public EventCallback<string> ValueChanged { get; set; }

    [Parameter]
    public ValidationState ValidationState { get; set; }

    [Parameter]
    public string Id { get; set; } = "";

    [Parameter]
    public ComponentSize Size { get; set; }

    [Parameter]
    public string Class { get; set; } = "";
    
    [Parameter]
    public bool ReadOnly { get; set; }
    
    [Parameter]
    public bool Disabled { get; set; }

    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? Attributes { get; set; }

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

        Attributes ??= [];

        if (!string.IsNullOrEmpty(Id))
            Attributes["id"] = Id;
        else
            Attributes.Remove("id");
        
        if (ReadOnly)
            Attributes["readonly"] = "readonly";
        else
            Attributes.Remove("readonly");
        
        Attributes["class"] =
            string.Join(
                " ",
                classes);
    }

    private async Task HandleInputAsync(
        ChangeEventArgs e)
    {
        await ValueChanged.InvokeAsync(e.Value?.ToString());
    }
}
