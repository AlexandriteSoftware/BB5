# Forms

Examples showcasing the Form component.

## Example

HTML:

```html
<form>
    <div class="mb-3">
        <label for="Email" class="form-label">
            Email
        </label>
        <input id="Email" placeholder="name@example.com" class="form-control">
    </div>
    <div class="mb-3">
        <label for="Notes" class="form-label">
            Notes
        </label>
        <textarea id="Notes" class="form-control"></textarea>
    </div>
</form>
```

Blazor:

```razor
@using System.ComponentModel.DataAnnotations

<BB5.Form Object="@Model"
          AutoSubmit="@true" />

@code {
    public class FormControlsExampleFormModel
    {
        [EmailAddress(
            ErrorMessage =
                "The Email field is not a valid e-mail address.")]
        [InputTextPlaceholder("name@example.com")]
        public string Email { get; set; } = "";

        [TextArea]
        public string Notes { get; set; } = "";
    }
    
    private FormControlsExampleFormModel Model { get; set; } = new();
}
```

## Sizing

HTML:

```html
<form>
    <div class="mb-3">
        <label for="Large" class="form-label fs-5">
            Large
        </label>
        <input id="Large" class="form-control form-control-lg"
               placeholder="Large input">
    </div>
    <div class="mb-3">
        <label for="Default" class="form-label">
            Default
        </label>
        <input id="Default" class="form-control"
               placeholder="Default input">
    </div>
    <div class="mb-3">
        <label for="Small" class="form-label small">
            Small
        </label>
        <input id="Small" class="form-control form-control-sm"
               placeholder="Small input">
    </div>
</form>
```

Blazor:

```razor
@using BB5.FormComponents

<BB5.Form Object="@Model"
          AutoSubmit="@true">
    <ControlsTemplate>
        <ControlsPlaceholder />
    </ControlsTemplate>
</BB5.Form>

@code {
    public class FormControlsSizingFormModel
    {
        [ComponentSize(ComponentSize.Large)]
        [InputTextPlaceholder("Large input")]
        public string Large { get; set; } = "";

        [InputTextPlaceholder("Default input")]
        public string Default { get; set; } = "";

        [ComponentSize(ComponentSize.Small)]
        [InputTextPlaceholder("Small input")]
        public string Small { get; set; } = "";
    }
    
    private FormControlsSizingFormModel Model { get; set; } = new();
}
```
