using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using BB5.FormComponents;
using BB5.Models;
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
    public EventCallback<FormModel> ModelCreated { get; set; }

    [Parameter]
    public EventCallback<FormActionContext> Submitted { get; set; }

    [Parameter]
    public object? SubmitContent { get; set; } = "Submit";

    [Parameter]
    public RenderFragment<FormModel>? ControlsTemplate { get; set; }

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

    private FormModel Model { get; set; } = new();

    private string? Error { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();

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

        if (Attributes is { } attributes)
        {
            foreach (var attribute in attributes)
            {
                FormAttributes[attribute.Key] =
                    attribute.Value;
            }
        }

        if (ReferenceEquals(_previousObject, Object))
        {
            return;
        }

        _previousObject = Object;

        Model.Clear();

        ModifiedObject = ShallowCopy(Object);

        if (Object is { } @object
            && ModifiedObject is { } modifiedObject)
        {
            Model.AddRange(
                FormControlModel.From(
                    @object,
                    modifiedObject,
                    async _ =>
                    {
                        if (AutoSubmit)
                            await HandleSubmitAsync();
                    }));
        }

        await ModelCreated.InvokeAsync(Model);
    }

    private async Task HandleSubmitAsync()
    {
        var valid = true;

        foreach (var control in Model.ToList())
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
                        .Select(item =>
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

    private RenderFragment<FormModel> EffectiveControlsTemplate =>
        ControlsTemplate
        ?? DefaultControlsTemplate;

    private RenderFragment<FormModel> DefaultControlsTemplate =>
        model =>
            builder =>
            {
                var seq = 0;
                builder.OpenComponent(++seq, typeof(ControlsPlaceholder));
                builder.CloseComponent();
            };

    private RenderFragment<FormControlModel> EffectiveControlTemplate =>
        ControlTemplate
        ?? DefaultControlTemplate;

    private RenderFragment<FormControlModel> DefaultControlTemplate { get; } =
        model =>
            builder =>
            {
                var seq = 0;
                builder.OpenElement(++seq, "div");
                builder.AddAttribute(++seq, "class", "mb-3");
                builder.OpenComponent(++seq, typeof(LabelPlaceholder));
                builder.CloseComponent();
                builder.OpenComponent(++seq, typeof(InputPlaceholder));
                builder.CloseComponent();
                builder.OpenComponent(++seq, typeof(FeedbackPlaceholder));
                builder.CloseComponent();
                builder.OpenComponent(++seq, typeof(TextPlaceholder));
                builder.CloseComponent();
                builder.CloseElement();
            };
}