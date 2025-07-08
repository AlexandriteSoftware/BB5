# Table

Table is a component for displaying tabular data in a structured format.

See the [Bootstrap documentation](https://getbootstrap.com/docs/5.3/content/tables/) for more details.

## Markup

Blazor:

```razor
@{
    var tableItems =
        new List<object> {
            new() { Name = "Item 1", Value = 10 },
            new() { Name = "Item 2", Value = 20 }
        };
}

<Table Items="@tableItems" />
```

HTML result:

```html
<table class="table">
    <thead>
        <tr>
            <th>Name</th>
            <th>Value</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td>Item 1</td>
            <td>10</td>
        </tr>
        <tr>
            <td>Item 2</td>
            <td>20</td>
        </tr>
    </tbody>
</table>
```
