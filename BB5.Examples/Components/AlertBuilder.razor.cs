namespace BB5.Examples.Components;

public class AlertProperties
{
    public AlertColor Color { get; set; }
    public string Content { get; set; } = "";
    public bool ContentIsMarkup { get; set; } = false;
    public bool Dismissible { get; set; }
}

public partial class AlertBuilder
{
    private AlertProperties Properties { get; set; } = new();
    private string? AlertClass { get; set; } = "";

    protected override void OnInitialized()
    {
        base.OnInitialized();

        Properties.Color = AlertColor.Primary;
        Properties.Content = "This is an alert.";
        Properties.Dismissible = false;
    }

    private void HandleStyleUpdate(
        string style)
    {
        AlertClass = style;
    }
}