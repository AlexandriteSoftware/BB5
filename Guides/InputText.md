# InputText

InputText component provides a single-line or multi-line text input. It renders either as `<input type="text" class="form-control">` or as `<textarea class="form-control" />`.

## Markup

### One row

```razor
<InputText />
```

```html
<input type="text" class="form-control">
```

### Multiple rows

```razor
<InputText Rows="3" />
```

```html
<textarea class="form-control"></textarea>
```

## Parameters

Parameters:

| Name | Type | Description |
|------|------|-------------|
| `Class` | `string` | Extra classes, added to the HTML's `class` attribute. |
| `Disabled` | `bool` | Whether the instance is disabled. |
| `Id` | `string` | The HTML's `id` attribute. |
| `ReadOnly` | `bool` | Whether the instance allows editing. |
| `Size` | `ComponentSize` | `Default`, `Small`, `Large` |
| `ValidationState` | `ValidationState` | `None`, `Valid`, of `Invalid`. |
| `Value` | `string` | The initial value. |
| `ValueChanged` | `EventCallback<string>` | Raised when value is updated on input. |

Unmatched parameters will be added as HTML tag attributes.

## See also

- [InputPassword]
- [InputDate]
- [InputLabel]
- [Form]
