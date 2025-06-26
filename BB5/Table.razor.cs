using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace BB5;

public partial class Table
{
    [Parameter]
    public object? Items { get; set; }
    
    [Parameter]
    public EventCallback<object> OnActivate { get; set; }

    [Parameter]
    public string Class { get; set; } = "";

    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? Attributes { get; set; }

    private List<object?> Objects { get; set; } = [];
    private List<string> Columns { get; set; } = [];
    private List<List<object?>> Rows { get; set; } = [];

    protected override void OnParametersSet()
    {
        base.OnParametersSet();
        
        var classes =
            new List<string>
            {
                "table"
            };

        if (!string.IsNullOrEmpty(Class))
        {
            classes.AddRange(
                Class.Split(
                    ',',
                    StringSplitOptions.RemoveEmptyEntries));
        }

        Attributes ??= [];

        Attributes["class"] =
            string.Join(
                " ",
                classes);

        UpdateState(Items);
    }

    private void UpdateState(
        object? items)
    {
        if (items is not IEnumerable enumerable)
        {
            Objects = [];
            Columns = [];
            Rows = [];
            return;
        }
        
        var columns = new List<string>();
        var rows = new List<List<object?>>();
        var objects = new List<object?>();

        foreach (var item in enumerable)
        {
            objects.Add(item);

            var row =
                new List<object?>(
                    Enumerable.Repeat("", columns.Count));

            rows.Add(row);

            var properties =
                item.GetType()
                    .GetProperties();

            foreach (var property in properties)
            {
                var index = columns.IndexOf(property.Name);
                if (index == -1)
                {
                    index = columns.Count;
                    columns.Add(property.Name);
                    row.Add(null);
                }

                row[index] = property.GetValue(item);
            }
        }
        
        foreach (var row in rows)
        {
            if (row.Count < columns.Count)
            {
                row.AddRange(
                    Enumerable.Repeat(
                        (object?)null,
                        columns.Count - row.Count));
            }
        }

        Objects = objects;
        Columns = columns;
        Rows = rows;
    }

    private async Task ActivateAsync(
        int rowIndex)
    {
        await OnActivate.InvokeAsync(
            Objects[rowIndex]);
    }
}