# BB5

BB5 is a set of Blazor components for Bootstrap 5.

It is different from another of the Blazor Bootstrap component libraries
in that it is not just a mapper around Bootstrap components. It focuses
on data flow rather than markup and glues together the Bootstrap components
and Blazor in a way that adds value to the developer.

Example:

```razor
<div class="mb-3">
    <Label Content="Title"
           For="@TitleId"/>
    <Input Id="@TitleId"
           Value="@TitleText"
           ValidationState="@TitleValidationState"
           ValueChanged="value => TitleEdited = value" />
    <Feedback Content="@TitleFeedback"
              ValidationState="@TitleValidationState" />
</div>
```

depending on the state of the component, will render as

```html
<div class="mb-3">
  <label for="title" class="form-label">Title</label>
  <input id="title" class="form-control" value="">
</div>
```

or

```html
<div class="mb-3">
  <label for="title" class="form-label">Title</label>
  <input id="title" class="form-control is-invalid" value="">
  <div class="invalid-feedback">Title is required</div>
</div>
```

In this project, as in all the others I maintain, I strive to follow
the highest quality standards — including meticulous code reviews,
unit tests, integration tests, and thorough documentation.

This level of quality comes at a cost. If you find this project useful,
please consider sponsoring it. Sponsorship is the best way to support
the project, ensure its continued development, and receive updates and
new features.
