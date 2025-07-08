using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace BB5;

public class FormActionContext
{
    public object? Object { get; set; }
    
    public IList<ValidationResult> Errors { get; init; } = [];
}

public partial class Form
{
    [Parameter]
    public object? Object { get; set; }
    
    [Parameter]
    public EventCallback<FormActionContext> Submitted { get; set; }

    [Parameter]
    public object? SubmitContent { get; set; } = "Submit";
    
    [Parameter]
    public RenderFragment<IEnumerable<FormControlModel>>? ControlsTemplate { get; set; }

    [Parameter]
    public RenderFragment<FormControlModel>? ControlTemplate { get; set; }

    [Parameter]
    public RenderFragment? Actions { get; set; }
    
    [Parameter]
    public bool AutoSubmit { get; set; }
    
    [Parameter]
    public string? Class { get; set; }

    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? Attributes { get; set; }
    
    private object? _previousObject;
    
    private Dictionary<string, object>? FormAttributes { get; set; }

    private object? ModifiedObject { get; set; }

    private List<FormControlModel> FormControls { get; set; } = [];
    
    private string? Error { get; set; }

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

        FormAttributes = [];
        
        if (classes.Count > 0)
        {
            FormAttributes["class"] =
                string.Join(
                    " ",
                    classes);
        }

        if (ReferenceEquals(_previousObject, Object))
        {
            return;
        }

        _previousObject = Object;
        
        FormControls.Clear();

        ModifiedObject = ShallowCopy(Object);

        if (Object is { } @object
            && ModifiedObject is { } modifiedObject)
        {
            FormControls.AddRange(
                FormControlModel.From(
                    @object,
                    modifiedObject,
                    async _ =>
                    {
                        if (AutoSubmit)
                            await HandleSubmitAsync();
                    }));
        }
    }

    private async Task HandleSubmitAsync()
    {
        var valid = true;

        foreach (var control in FormControls)
        {
            await control.Update();
            valid &= control.ValidationState != ValidationState.Invalid;
        }
        
        if (!valid)
            return;

        Error = null;
        
        var context =
            new FormActionContext
            {
                Object = ModifiedObject,
                Errors = []
            };

        await Submitted.InvokeAsync(context);
        
        if (context.Errors.Count > 0)
        {
            Error =
                string.Join(
                    " ",
                    context
                        .Errors
                        .Select(
                            item =>
                                item.ErrorMessage
                                ?? "Unknown error"));
        }
    }
    
    private static object? ShallowCopy(
        object? obj)
    {
        if (obj == null)
            return null;

        var method =
            typeof(object)
                .GetMethod(
                    "MemberwiseClone",
                    BindingFlags.Instance
                    | BindingFlags.NonPublic);

        return method!.Invoke(obj, null);
    }
    
    private RenderFragment<IEnumerable<FormControlModel>> GetControlsTemplate()
    {
        return ControlsTemplate
               ?? GetDefaultControlsTemplate();
    }

    private RenderFragment<IEnumerable<FormControlModel>> GetDefaultControlsTemplate()
    {
        return items =>
            builder =>
            {
                var controlTemplate = GetControlTemplate();
                foreach (var item in items)
                {
                    controlTemplate(item)(builder);
                }
            };
    }
    
    private RenderFragment<FormControlModel> GetControlTemplate()
    {
        return ControlTemplate
               ?? GetDefaultControlTemplate();
    }

    private RenderFragment<FormControlModel> GetDefaultControlTemplate()
    {
        return item =>
            builder =>
            {
                builder.OpenElement(0, "div");
                builder.AddAttribute(1, "class", "mb-3");
                builder.OpenComponent(2, typeof(FormControl));
                builder.AddAttribute(3, "Model", item);
                builder.CloseComponent();
                builder.CloseElement();
            };
    }
}