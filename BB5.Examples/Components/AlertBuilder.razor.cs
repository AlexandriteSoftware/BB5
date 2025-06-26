namespace BB5.Examples.Components;

public class AlertProperties
{
    public AlertColor Color { get; set; }
    public string Content { get; set; }
    public bool Dismissible { get; set; }
}

public partial class AlertBuilder
{
    private AlertProperties? AlertProperties { get; set; }
    private string? AlertClass { get; set; } = "";
    private AlertColor AlertColor { get; set; }
    private string AlertContent { get; set; } = "";
    private bool AlertDismissible { get; set; }

    protected override void OnInitialized()
    {
        base.OnInitialized();

        AlertProperties =
            new AlertProperties
            {
                Color = AlertColor.Primary,
                Content = "This is an alert.",
                Dismissible = false
            };
    }

    private void HandleStyleUpdate(
        string style)
    {
        AlertClass = style;
    }

    private void AlertPropertiesUpdated(
        object? @object)
    {
        if (@object is not AlertProperties properties)
            return;
        
        AlertColor = properties.Color;
        AlertContent = properties.Content;
        AlertDismissible = properties.Dismissible;
    }
}