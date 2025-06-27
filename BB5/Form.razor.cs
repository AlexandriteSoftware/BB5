using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace BB5;

internal class FormControlInfo
{
    public string Id { get; init; } = "";

    public string DisplayName { get; init; } = "";
    
    public bool IsReadOnly { get; init; } = false;
    
    public static FormControlInfo From(
        PropertyInfo propertyInfo)
    {
        var displayNameAttribute =
            propertyInfo
                .GetCustomAttributes(
                    typeof(DisplayNameAttribute),
                    false)
                .FirstOrDefault() as DisplayNameAttribute;

        var displayName =
            displayNameAttribute?.DisplayName
            ?? propertyInfo.Name;
        
        var isReadOnly = !propertyInfo.CanWrite;

        return new()
        {
            Id = propertyInfo.Name,
            DisplayName = displayName,
            IsReadOnly = isReadOnly
        };
    }
}

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