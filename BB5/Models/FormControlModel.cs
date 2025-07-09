using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace BB5.Models;

/// <summary>
///     Supported types of the Input control.
/// </summary>
/// <remarks>
///     See https://html.spec.whatwg.org/multipage/input.html#states-of-the-type-attribute
/// </remarks>
public enum InputType
{
    Text,
    TextArea,
    Password,
    Checkbox,
    Radio,
    Select,
    Date
}

public class FormControlModel
{
    public required string Name { get; init; }
    
    public string Id { get; init; } = "";

    public string DisplayName { get; init; } = "";

    public bool IsReadOnly { get; init; }

    public bool IsPassword { get; init; }

    public bool IsRequired { get; init; }

    public bool IsTextArea { get; init; }

    public string RequiredMessage { get; init; } = "";

    public bool HasLabel { get; init; }

    public bool HasFeedback { get; init; }

    public string Description { get; init; } = "";

    public required object Object { get; init; }

    public required object ModifiedObject { get; set; }

    public string Text { get; set; } = "";
    
    public string ModifiedText { get; set; } = "";

    public Func<object, Task>? ModifiedHandlerAsync { get; init; }

    public required PropertyInfo PropertyInfo { get; init; }

    public IReadOnlyList<Func<string, ValidationResult?>> Validators { get; init; } = [];

    public string ValidationFeedback { get; set; } = "";

    public ValidationState ValidationState { get; set; }
    
    public LabelProperties? LabelProperties { get; set; }

    public object? ControlProperties { get; set; }
    
    public static IList<FormControlModel> From(
        object @object,
        object modifiedObject,
        Func<object, Task> modifiedAsync)
    {
        var list = new List<FormControlModel>();

        foreach (var property in @object.GetType().GetProperties())
        {
            var controlInfo =
                From(
                    property,
                    @object,
                    modifiedObject,
                    modifiedAsync);

            list.Add(controlInfo);
        }

        return list;
    }

    public static FormControlModel From(
        PropertyInfo propertyInfo,
        object @object,
        object modifiedObject,
        Func<object, Task> modifiedAsync)
    {
        var displayNameAttribute =
            propertyInfo
                .GetCustomAttributes<DisplayNameAttribute>()
                .FirstOrDefault();

        var displayName =
            displayNameAttribute?.DisplayName
            ?? propertyInfo.Name;

        var isReadOnly = !propertyInfo.CanWrite;

        var isPassword =
            propertyInfo
                .GetCustomAttributes<PasswordPropertyTextAttribute>()
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
            nullability.ReadState == NullabilityState.NotNull
            && propertyType != typeof(string);

        var requiredAttribute =
            propertyInfo
                .GetCustomAttributes<RequiredAttribute>()
                .FirstOrDefault();

        var isRequired =
            requiredAttribute != null
            || isRequiredValueType
            || isRequiredReferenceType;

        var isTextArea =
            propertyInfo
                .GetCustomAttributes<TextAreaAttribute>()
                .Any();

        var hasLabel =
            typeof(bool) != propertyType;

        var hasFeedback =
            !isReadOnly;

        var description =
            propertyInfo
                .GetCustomAttributes<DescriptionAttribute>()
                .FirstOrDefault();

        var requiredMessage =
            requiredAttribute?.ErrorMessage
            ?? $"{displayName} is required.";

        var validators =
            propertyInfo
                .GetCustomAttributes<ValidationAttribute>()
                .Select(item =>
                    new Func<string, ValidationResult?>(value =>
                        item.GetValidationResult(
                            value,
                            new ValidationContext(
                                @object,
                                null,
                                null))))
                .ToList();

        var text =
            GetText(
                @object,
                propertyInfo);

        InputType inputType;
        if (propertyInfo.PropertyType.IsEnum)
            inputType = InputType.Select;
        else if (typeof(bool) == propertyInfo.PropertyType)
            inputType = InputType.Checkbox;
        else if (typeof(DateOnly) == propertyInfo.PropertyType
                || typeof(DateOnly?) == propertyInfo.PropertyType)
        {
            inputType = InputType.Date;
        }
        else if (typeof(string) == propertyInfo.PropertyType
                 && isTextArea)
        {
            inputType = InputType.TextArea;
        }
        else
        {
            inputType = InputType.Text;
        }

        object controlProperties
            = inputType switch
            {
                InputType.Text => new InputTextProperties(),
                InputType.TextArea => new TextAreaProperties(),
                InputType.Password => new InputPasswordProperties(),
                InputType.Checkbox => new InputCheckboxProperties(),
                InputType.Radio => new InputRadioProperties(),
                InputType.Select => new SelectProperties(),
                InputType.Date => new InputDateProperties(),
                _ =>
                    throw new NotSupportedException(
                        $"Input type '{inputType}' is not supported.")
            };

        if (controlProperties is InputTextProperties inputTextProperties)
        {
            if (propertyInfo.GetCustomAttribute<InputTextPlaceholderAttribute>() is { } placeholderAttribute)
                inputTextProperties.Placeholder = placeholderAttribute.Text;
            inputTextProperties.ReadOnly = isReadOnly;
        }

        if (controlProperties is ISizeableComponent sizeableComponent)
        {
            if (propertyInfo.GetCustomAttribute<ComponentSizeAttribute>() is { } sizeAttribute)
                sizeableComponent.Size = sizeAttribute.Size;
        }

        var labelProperties =
            hasLabel
                ? new LabelProperties()
                : null;

        if (labelProperties != null)
        {
            if (propertyInfo.GetCustomAttribute<ComponentSizeAttribute>() is { } sizeAttribute)
                labelProperties.Size = sizeAttribute.Size;
        }
        
        return new()
        {
            Name = propertyInfo.Name,
            PropertyInfo = propertyInfo,
            Id = propertyInfo.Name,
            DisplayName = displayName,
            Description = description?.Description ?? "",
            IsPassword = isPassword,
            IsReadOnly = isReadOnly,
            IsRequired = isRequired,
            IsTextArea = isTextArea,
            RequiredMessage = requiredMessage,
            HasLabel = hasLabel,
            HasFeedback = hasFeedback,
            Object = @object,
            Text = text,
            ModifiedText = text,
            Validators = validators,
            ModifiedObject = modifiedObject,
            ModifiedHandlerAsync = modifiedAsync,
            LabelProperties = labelProperties,
            ControlProperties = controlProperties
        };
    }

    public async Task SetTextAsync(
        string value)
    {
        ValidationFeedback = "";
        ValidationState = ValidationState.None;
        
        var modified =
            !string.Equals(
                ModifiedText,
                value,
                StringComparison.Ordinal);

        ModifiedText = value;
        
        if (IsRequired
            && string.IsNullOrEmpty(value))
        {
            Error(RequiredMessage);
            return;
        }

        foreach (var validator in Validators)
        {
            var validationResult = validator(value);
            if (validationResult != null
                && validationResult != ValidationResult.Success)
            {
                Error(
                    validationResult.ErrorMessage
                    ?? "Unknown error");

                return;
            }
        }

        if (!modified)
            return;

        if (PropertyInfo.PropertyType.IsEnum)
        {
            PropertyInfo.SetValue(
                ModifiedObject,
                Enum.Parse(
                    PropertyInfo.PropertyType,
                    value));
        }

        if (typeof(bool) == PropertyInfo.PropertyType)
        {
            var typeValue = value == "on";

            PropertyInfo.SetValue(
                ModifiedObject,
                typeValue);
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
                ModifiedObject,
                typeValue);
        }

        if (typeof(int?) == PropertyInfo.PropertyType)
        {
            if (string.IsNullOrEmpty(value))
            {
                PropertyInfo.SetValue(
                    ModifiedObject,
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
                    ModifiedObject,
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
                ModifiedObject,
                typeValue);
        }

        if (typeof(decimal?) == PropertyInfo.PropertyType)
        {
            if (string.IsNullOrEmpty(value))
            {
                PropertyInfo.SetValue(
                    ModifiedObject,
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
                    ModifiedObject,
                    typeValue);
            }
        }

        if (typeof(string) == PropertyInfo.PropertyType)
        {
            PropertyInfo.SetValue(
                ModifiedObject,
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
                ModifiedObject,
                typeValue);
        }

        if (typeof(DateOnly?) == PropertyInfo.PropertyType)
        {
            if (string.IsNullOrEmpty(value))
            {
                PropertyInfo.SetValue(
                    ModifiedObject,
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
                    ModifiedObject,
                    typeValue);
            }
        }

        if (ModifiedHandlerAsync != null)
            await ModifiedHandlerAsync.Invoke(ModifiedObject);
    }

    private void Error(
        string text)
    {
        ValidationFeedback = text;
        ValidationState = ValidationState.Invalid;
    }

    public async Task Update()
    {
        await SetTextAsync(ModifiedText);
    }

    private static string GetText(
        object @object,
        PropertyInfo propertyInfo)
    {
        if (propertyInfo.PropertyType.IsEnum)
        {
            var propertyValue = propertyInfo.GetValue(@object);
            return Enum.GetName(propertyInfo.PropertyType, propertyValue!)!;
        }

        if (typeof(bool) == propertyInfo.PropertyType)
        {
            return propertyInfo.GetValue(@object) is true
                ? "on"
                : "off";
        }

        if (typeof(int) == propertyInfo.PropertyType)
        {
            var intValue =
                (int)propertyInfo.GetValue(@object)!;

            return Convert.ToString(
                intValue,
                CultureInfo.InvariantCulture);
        }

        if (typeof(int?) == propertyInfo.PropertyType)
        {
            var intValue =
                (int?)propertyInfo.GetValue(@object);

            return intValue == null
                ? ""
                : Convert.ToString(
                    intValue,
                    CultureInfo.InvariantCulture)!;
        }

        if (typeof(decimal) == propertyInfo.PropertyType)
        {
            var decimalValue =
                (decimal)(propertyInfo.GetValue(@object) ?? 0);

            return Convert.ToString(
                decimalValue,
                CultureInfo.InvariantCulture);
        }

        if (typeof(decimal?) == propertyInfo.PropertyType)
        {
            var decimalValue =
                (decimal?)propertyInfo.GetValue(@object);

            return decimalValue == null
                ? ""
                : Convert.ToString(
                    decimalValue,
                    CultureInfo.InvariantCulture)!;
        }

        if (typeof(string) == propertyInfo.PropertyType)
        {
            return propertyInfo.GetValue(@object)?.ToString() ?? "";
        }

        if (typeof(DateOnly) == propertyInfo.PropertyType)
        {
            return propertyInfo.GetValue(@object) is DateOnly dateOnly
                ? dateOnly.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)
                : "";
        }

        if (typeof(DateOnly?) == propertyInfo.PropertyType)
        {
            return propertyInfo.GetValue(@object) is DateOnly dateOnlyValue
                ? dateOnlyValue.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)
                : "";
        }

        return "";
    }
}