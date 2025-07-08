# Alert

Dismissible message panel that provides feedback to the user.

See the [Bootstrap documentation](https://getbootstrap.com/docs/5.3/components/alerts/)
for more details.

## Example

Blazor:

```razor
<Alert Color="AlertColor.Success"
       Class="m-3"
       Dismissible="@true"
       Content="@((MarkupString)"<b>Success!</b>")" />
```

HTML result:

```html
<div class="alert alert-success" role="alert">
    <b>Success!</b>
    <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
</div>
```
