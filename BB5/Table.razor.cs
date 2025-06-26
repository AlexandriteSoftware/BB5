using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Components;

namespace BB5;

public partial class Table
{
    [Parameter]
    public string Class { get; set; } = "";

    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? Attributes { get; set; }

    [Parameter]
    public object? Items { get; set; }
    
    private List<string> Columns { get; set; } = new();
    private List<List<object?>> Rows { get; set; } = new();

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

        (Columns, Rows) = GetCells(Items);
    }

    private (List<string> Columns, List<List<object?>> Rows) GetCells(
        object? items)
    {
        var columns = new List<string>();
        var rows = new List<List<object?>>();

        if (items is not IEnumerable enumerable)
            return ([], []);
        
        foreach (var item in enumerable)
        {
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
        
        return (columns, rows);
    }
}