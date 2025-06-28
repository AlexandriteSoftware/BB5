# Form

Form component corresponds to HTML element `<form>`.

When the Form's parameter `Object` is set, the form generates controls the object's properties.
Generation is controlled by data annotation attributes, property type, getter and setter.

| Order | Annotation | Description |
|-------|------------|-------------|
|     1 | `[DisplayName(...)]` | Sets the label for the field. |
|     2 | `Nullable<...>` or `?` | Marks the field as optional. |
|     3 | `[Required]` | Marks the field as required. |
|     4 | `Type` | Sets the type of the field. Supported types are `string`, `int`, `DateOnly`, `bool`, and `decimal`. |
|     5 | `{ get; }` | Marks the field as read-only. |
