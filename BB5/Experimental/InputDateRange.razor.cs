using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace BB5.Experimental;

public class InputDateRangeOptions
{
    public string Text { get; set; } = "";
    public DateOnly? Begin { get; set; }
    public DateOnly? End { get; set; }
}

public partial class InputDateRange
{
    [Parameter]
    public DateOnly? Begin { get; set; }
    
    [Parameter]
    public EventCallback<DateOnly?> BeginChanged { get; set; }

    [Parameter]
    public DateOnly? End { get; set; }
    
    [Parameter]
    public EventCallback<DateOnly?> EndChanged { get; set; }
    
    [Parameter]
    public IReadOnlyList<InputDateRangeOptions> Options { get; set; } = [];

    [Parameter]
    public string Class { get; set; } = "";

    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? Attributes { get; set; }

    private string BeginText { get; set; } = "";
    private string EndText { get; set; } = "";
    
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        var classes =
            new List<string>
            {
                "input-group"
            };

        if (!string.IsNullOrEmpty(Class))
        {
            classes.AddRange(
                Class.Split(
                    ' ',
                    StringSplitOptions.RemoveEmptyEntries));
        }

        Attributes ??= [];

        Attributes["role"] = "alert";

        Attributes["class"] =
            string.Join(
                " ",
                classes);
    }

    private async Task BeginChangedAsync(
        string value)
    {
        await BeginChanged.InvokeAsync(DateOnly.Parse(value));
    }
    
    private async Task EndChangedAsync(
        string value)
    {
        await EndChanged.InvokeAsync(DateOnly.Parse(value));
    }
    
    private void HandleOptionClick(
        InputDateRangeOptions item)
    {
        BeginText = item.Begin?.ToString("yyyy-MM-dd") ?? "";
        EndText = item.End?.ToString("yyyy-MM-dd") ?? "";
    }

}