using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace BB5;

public class Option
{
    public string Value { get; set; } = "";
    public bool Selected { get; set; }
    public string Text { get; set; } = "";
}

public partial class Select
{
    [Parameter]
    public string Value { get; set; } = "";

    [Parameter]
    public EventCallback<string> ValueChanged { get; set; }

    [Parameter]
    public RenderFragment? ChildContent { get; set; }
    
    [Parameter]
    public object? Items { get; set; }

    [Parameter]
    public string Id { get; set; } = "";

    [Parameter]
    public ValidationState ValidationState { get; set; }

    [Parameter]
    public bool ReadOnly { get; set; }
    
    [Parameter]
    public bool Disabled { get; set; }
    
    [Parameter]
    public ComponentSize Size { get; set; }
    
    [Parameter]
    public bool Multiple { get; set; }
    
    [Parameter]
    public int Rows { get; set; } = 4;

    [Parameter]
    public string Class { get; set; } = "";
    
    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? Attributes { get; set; }
    
    private List<Option> Options { get; set; } = new();

    protected override void OnParametersSet()
    {
        base.OnParametersSet();
        
        var classes =
            new List<string>
            {
                "form-select"
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
                classes.Add("form-select-sm");
                break;
            case ComponentSize.Large:
                classes.Add("form-select-lg");
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

        if (Multiple)
        {
            Attributes["multiple"] = "multiple";
            Attributes["size"] = Rows;
        }
        else
        {
            Attributes.Remove("multiple");
            Attributes.Remove("size");
        }

        Attributes["class"] =
            string.Join(
                " ",
                classes);

        Options = GetOptions(Items);
    }

    private async Task HandleInputAsync(
        ChangeEventArgs e)
    {
        await ValueChanged.InvokeAsync(e.Value?.ToString());
    }
    
    private List<Option> GetOptions(
        object? items)
    {
        if (items is not IEnumerable enumerable)
            return [];
        
        var options = new List<Option>();

        foreach (var item in enumerable)
        {
            if (item == null)
            {
                options.Add(
                    new Option
                    {
                        Selected = false,
                        Text = "",
                        Value = ""
                    });
                
                continue;
            }

            var text =
                item.ToString()
                ?? "";

            options.Add(
                new Option
                {
                    Selected = false,
                    Text = text,
                    Value = text
                });
        }
        
        return options;
    }
}