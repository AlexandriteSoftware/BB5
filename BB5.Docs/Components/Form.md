# Form

_[Back to Guides](../Overview.md)_

Form component corresponds to HTML element `<form>`.

When the Form's parameter `Object` is set, the form generates controls the object's properties.
Generation is controlled by data annotation attributes, property type, getter and setter.

```razor
<Form Object="@user"
      Submitted="_ => Save(user)" />
```

```html
<form>
    <div class="mb-3">
        <label class="form-label" for="...">Name</label>
        <input class="form-control" id="...">
    </div>
    <div class="mb-3">
        ...
    </div>
    <button class="btn btn-primary">Submit</button>
</form>
```

While editing, form's controls update the object.

The user can signal that editing is complete by submitting the form. The form then validates
the data and invokes `Submitted` event handler.
