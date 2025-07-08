# Form

Form component allows user to edit, validate, and submit date. It wraps
the HTML `<form>` element, and provides a way to generate form controls
based on an object model.

When the Form's parameter `Object` is set, the form generates controls
from the object's properties.

The component creates a shallow copy of the object, updates it after
the controls' data is validated, and invokes the `Submitted` event
handler.

See the [Bootstrap documentation](https://getbootstrap.com/docs/5.3/forms/overview/)
for more details.

## Markup

Blazor:

```razor
<Form Object="@user"
      Submitted="@(user => Save(user))" />
```

HTML result:

```html
<form>
    <div class="mb-3">
        <label class="form-label" for="...">Name</label>
        <input class="form-control" id="...">
    </div>
    <div class="mb-3">
        ...
    </div>
    <div class="mb-3">
        <button class="btn btn-primary">Submit</button>
    </div>
</form>
```
