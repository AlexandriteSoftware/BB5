using System.ComponentModel;

namespace BB5.Examples.Builders;

public class AlertProperties
{
    public AlertColor Color { get; set; }

    public string Content { get; set; } = "";
    
    [DisplayName("Content is markup (HTML)")]
    public bool ContentIsMarkup { get; set; } = false;

    public bool Dismissible { get; set; }
}

public partial class AlertBuilder
{
    private object? EditProperties { get; set; }

    private object? Properties { get; set; }

    private string Class { get; set; } = "";

    protected override void OnInitialized()
    {
        base.OnInitialized();

        EditProperties =
            new AlertProperties
            {
                Color = AlertColor.Primary,
                Content = "This is an alert.",
                Dismissible = false
            };

        Properties = EditProperties;
    }
}