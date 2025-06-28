using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace BB5;

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
    
    private List<FormControlModel> FormControls { get; set; } = [];

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
        
        FormControls.Clear();

        if (Object is {} @object)
        {
            foreach (var property in @object.GetType().GetProperties())
            {
                var controlInfo =
                    FormControlModel.From(
                        property,
                        @object,
                        async () => await OnModified.InvokeAsync());
                
                FormControls.Add(controlInfo);
            }
        }
    }

    private async Task HandleSubmit()
    {
        await OnSubmit.InvokeAsync();
    }
}