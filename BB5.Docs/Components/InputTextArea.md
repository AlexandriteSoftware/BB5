# InputTextArea

InputTextArea component provides a multi-line text input. It renders as `<textarea class="form-control" />`.

## Markup

Blazor:

```razor
<InputText Rows="3" />
```

HTML result:

```html
<textarea class="form-control"></textarea>
```

## Parameters

Parameters:

| Name              | Type                    | Description                                           |
|-------------------|-------------------------|-------------------------------------------------------|
| `Class`           | `string`                | Extra classes, added to the HTML's `class` attribute. |
| `Disabled`        | `bool`                  | Whether the instance is disabled.                     |
| `Id`              | `string`                | The HTML's `id` attribute.                            |
| `ReadOnly`        | `bool`                  | Whether the instance allows editing.                  |
| `Size`            | `ComponentSize`         | `Default`, `Small`, `Large`                           |
| `ValidationState` | `ValidationState`       | `None`, `Valid`, of `Invalid`.                        |
| `Value`           | `string`                | The initial value.                                    |
| `ValueChanged`    | `EventCallback<string>` | Raised when value is updated on input.                |

Unmatched parameters will be added as HTML attributes.

## See also

- [InputText](InputText.md)
- [Form](Form.md)
- [FormControl](FormControl.md)
