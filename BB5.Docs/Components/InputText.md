# InputText

InputText component provides a single-line text input. It renders as `<input type="text">`.

## Markup

Blazor:

```razor
<InputText />
```

HTML result:

```html
<input type="text" class="form-control">
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

- [InputTextArea](InputTextArea.md)
- [Form](Form.md)
- [FormControl](FormControl.md)
