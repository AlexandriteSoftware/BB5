# FormControl

FormControl is a component that generates a form control for a property of an object.

```razor
<FormControl Object="Model"
             Property="nameof(Model.Name)"
             ValueUpdated="..." />
```

FormControl updates the object's value and calls `ValueUpdated` after the validation
criteria are met and the user has finished editing the control.

Example of the FormControl structure for string property:

```razor
<InputLabel Content="@model.Title" />
<InputText ... />
<Feedback ... />
```

## Parameters

FormControl's parameters are used to configure the control. They are initialised
from object's property attributes and type.

When the attribute `[CopyAttributesFrom]` si present, the builder looks for
the attributes applied to the referenced property's type.

### `Title`

Control's label and control's name in validation messages.

Initialised from `[DisplayName(...)]` or property name.

### `ReadOnly`

Whether the control is read-only.

Initialised by checking existence of public setter.

### `Password`

Whether the control is a password input.

Initialised by checking `[PasswordPropertyText]`.

### `Required`

Whether the control's value can be empty/null.

Intialised by checking `[Required]` attribute first, then by checking if the property is
of `Nullable<...>` type, or a non-nullable reference type when `#nullable enable` is set.

If the `[Required(...)]` attribute is set with a message, that message is used. Otherwise
the standard message "{Name} is required" is used.

### `DataType`

Whether the control's value is a date, number, or text.

Initialised from property's type. Supported types are `string`, `int`, `DateOnly`,
`bool`, and `decimal`.

### `Multiline`

Where the control is a multi-line text input (`<textarea>`).

Only applicable for the `string` data type. Cannot be applied to **Password** inputs.

Initialised by checking existence of `[MultilineText]`.

### `Description`

Whether the control has a description text.

Initialised from `[Description(...)]`.

### `TrimText`

Whether the control's text is trimmed before validation.

Initialised by checking existence of `[TrimText]`.

## Validation

When the user's input is validated, two model's properties are updated:

- `ValidationState` - `Normal`, `Invalid`, or `Valid`
- `ValidationMessage` - `Feedback`'s control content. 

## See also

- [Form](Form.md)
- [InputText](InputTextArea.md)
- [InputTextArea](InputTextArea.md)
