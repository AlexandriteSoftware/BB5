using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;

namespace BB5;

public partial class Feedback
{
    [Parameter]
    public ValidationState ValidationState { get; set; }

    [Parameter]
    public object? Content { get; set; }
    
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    [Parameter]
    public string Class { get; set; } = "";

    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? Attributes { get; set; }
    
    private List<object> List { get; set; } = [];

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        var classes =
            new List<string>();

        switch (ValidationState)
        {
            case ValidationState.Valid:
                classes.Add("valid-feedback");
                break;
            case ValidationState.Invalid:
                classes.Add("invalid-feedback");
                break;
            case ValidationState.None:
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

        List = GetErrors(Content);
    }
    
    private List<object> GetErrors(
        object? items)
    {
        switch (items)
        {
            case string singleString:
                return [singleString];
            case MarkupString singleMarkupString:
                return [singleMarkupString];
        }

        if (items is not IEnumerable enumerable)
            return [];
        
        var list = new List<object>();

        foreach (var item in enumerable)
        {
            switch (item)
            {
                case string stringValue:
                    list.Add(stringValue);
                    break;
                case MarkupString markupStringValue:
                    list.Add(markupStringValue);
                    break;
            }
        }
        
        return list;
    }
}
