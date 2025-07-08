# Badge

Component for displaying contextual information in a small badge format.

See the [Bootstrap documentation](https://getbootstrap.com/docs/5.3/components/badge/)
for more details.

## Example

Blazor:

```razor
@{ var notificationsCount = 5; }
Notifications
<Badge Color="TextBackgroundColor.Primary"
       Content="@notificationsCount" />
```

HTML result:

```html
Notifications <span class="badge text-bg-primary">5</span>
```
