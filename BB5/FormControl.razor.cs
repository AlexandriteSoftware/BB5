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

public class FormControlModel
{
    public string Id { get; init; } = "";

    public string DisplayName { get; init; } = "";
    
    public bool IsReadOnly { get; init; }

    public bool IsPassword { get; init; }

    public bool IsRequired { get; init; }
    
    public bool IsMultilineText { get; init; }
    
    public string RequiredMessage { get; init; } = "";
    
    public bool HasLabel { get; init; }

    public bool HasFeedback { get; init; }

    public string Description { get; init; } = "";

    public bool TrimText { get; init; }

    public required object Object { get; init; }
    
    public Func<Task>? SetValueAsyncHandler { get; init; }

    public required PropertyInfo PropertyInfo { get; init; }

    public string ValidationFeedback { get; set; } = "";

    public ValidationState ValidationState { get; set; }

    public static IList<FormControlModel> From(
        object @object)
    {
        var list = new List<FormControlModel>();

        foreach (var property in @object.GetType().GetProperties())
        {
            var controlInfo =
                From(
                    property,
                    @object,
                    () => Task.CompletedTask);
            
            list.Add(controlInfo);
        }

        return list;
    }

    public static FormControlModel From(
        PropertyInfo propertyInfo,
        object @object,
        Func<Task> setValueAsync)
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
        
        var isPassword =
            propertyInfo
                .GetCustomAttributes(
                    typeof(PasswordPropertyTextAttribute),
                    false)
                .Any();

        var propertyType =
            propertyInfo.PropertyType;
        
        var isNullableValueType =
            propertyType.IsGenericType
            && propertyType.GetGenericTypeDefinition() == typeof(Nullable<>);
        
        var isRequiredValueType =
            propertyType.IsValueType
            && !isNullableValueType;
        
        var nullabilityContext =
            new NullabilityInfoContext();
        var nullability =
            nullabilityContext.Create(propertyInfo);
        var isRequiredReferenceType =
            nullability.ReadState == NullabilityState.NotNull;
        
        var requiredAttribute =
            propertyInfo
                .GetCustomAttributes(
                    typeof(RequiredAttribute),
                    false)
                .FirstOrDefault() as RequiredAttribute;

        var isRequired =
            requiredAttribute != null
            || isRequiredValueType
            || isRequiredReferenceType;
        
        var isMultilineText =
            propertyInfo
                .GetCustomAttributes(
                    typeof(MultilineTextAttribute),
                    false)
                .Any();

        var hasLabel =
            typeof(bool) != propertyType;

        var hasFeedback =
            !isReadOnly;
        
        var description =
            propertyInfo
                .GetCustomAttributes(
                    typeof(DescriptionAttribute),
                    false)
                .FirstOrDefault() as DescriptionAttribute;
        
        var trimText =
            propertyInfo
                .GetCustomAttributes(
                    typeof(TrimTextAttribute),
                    false)
                .Any();

        var requiredMessage =
            requiredAttribute?.ErrorMessage
            ?? $"{displayName} is required.";

        return new()
        {
            PropertyInfo = propertyInfo,
            Id = propertyInfo.Name,
            DisplayName = displayName,
            Description = description?.Description ?? "",
            IsPassword = isPassword,
            IsReadOnly = isReadOnly,
            IsRequired = isRequired,
            IsMultilineText = isMultilineText,
            RequiredMessage = requiredMessage,
            TrimText = trimText,
            HasLabel = hasLabel,
            HasFeedback = hasFeedback,
            Object = @object,
            SetValueAsyncHandler = setValueAsync
        };
    }

    public async Task SetValueAsync(
        string value)
    {
        if (IsRequired
            && string.IsNullOrEmpty(value))
        {
            Error(RequiredMessage);
            return;
        }

        if (PropertyInfo.PropertyType.IsEnum)
        {
            PropertyInfo.SetValue(
                Object,
                Enum.Parse(
                    PropertyInfo.PropertyType,
                    value));
        }

        if (typeof(int) == PropertyInfo.PropertyType)
        {
            if (!int.TryParse(
                    value,
                    CultureInfo.InvariantCulture,
                    out var typeValue))
            {
                Error("Invalid integer value.");
                return;
            }

            PropertyInfo.SetValue(
                Object,
                typeValue);
        }
        
        if (typeof(int?) == PropertyInfo.PropertyType)
        {
            if (string.IsNullOrEmpty(value))
            {
                PropertyInfo.SetValue(
                    Object,
                    null);
            }
            else
            {
                if (!int.TryParse(
                        value,
                        CultureInfo.InvariantCulture,
                        out var typeValue))
                {
                    Error("Invalid integer value.");
                    return;
                }

                PropertyInfo.SetValue(
                    Object,
                    typeValue);
            }
        }


        if (typeof(decimal) == PropertyInfo.PropertyType)
        {
            if (!decimal.TryParse(
                    value,
                    CultureInfo.InvariantCulture,
                    out var typeValue))
            {
                Error("Invalid decimal value.");
                return;
            }

            PropertyInfo.SetValue(
                Object,
                typeValue);
        }

        if (typeof(decimal?) == PropertyInfo.PropertyType)
        {
            if (string.IsNullOrEmpty(value))
            {
                PropertyInfo.SetValue(
                    Object,
                    null);
            }
            else
            {
                if (!decimal.TryParse(
                        value,
                        CultureInfo.InvariantCulture,
                        out var typeValue))
                {
                    Error("Invalid decimal value.");
                    return;
                }

                PropertyInfo.SetValue(
                    Object,
                    typeValue);
            }
        }

        if (typeof(string) == PropertyInfo.PropertyType)
        {
            PropertyInfo.SetValue(
                Object,
                value);
        }
        
        if (typeof(DateOnly) == PropertyInfo.PropertyType)
        {
            if (!DateOnly.TryParseExact(
                    value,
                    "yyyy-MM-dd",
                    out var typeValue))
            {
                Error("Invalid date value.");
                return;
            }

            PropertyInfo.SetValue(
                Object,
                typeValue);
        }
        
        if (typeof(DateOnly?) == PropertyInfo.PropertyType)
        {
            if (string.IsNullOrEmpty(value))
            {
                PropertyInfo.SetValue(
                    Object,
                    null);
            }
            else
            {
                if (!DateOnly.TryParseExact(
                        value,
                        "yyyy-MM-dd",
                        out var typeValue))
                {
                    Error("Invalid date value.");
                    return;
                }

                PropertyInfo.SetValue(
                    Object,
                    typeValue);
            }
        }

        if (SetValueAsyncHandler != null)
            await SetValueAsyncHandler.Invoke();
    }

    private void Error(
        string text)
    {
        ValidationFeedback = text;
        ValidationState = ValidationState.Invalid;
    }
}

public partial class FormControl
{
    [Parameter]
    public FormControlModel? Model { get; set; }

    [Parameter]
    public EventCallback OnModified { get; set; }
}