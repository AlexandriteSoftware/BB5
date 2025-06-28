using System.ComponentModel;

namespace BB5.Examples.Components;

public class ButtonProperties
{
    public ButtonVariant Variant { get; set; }
    public string Content { get; set; } = "";

    [DisplayName("Content is markup (HTML)")]
    public bool ContentIsMarkup { get; set; } = false;
    public bool Outline { get; set; }
}

public partial class ButtonBuilder
{
    private ButtonProperties Properties { get; set; } = new();
    private string? ButtonClass { get; set; } = "";

    protected override void OnInitialized()
    {
        base.OnInitialized();

        Properties.Variant = ButtonVariant.Primary;
        Properties.Content = "Button";
        Properties.Outline = false;
    }

    private void HandleStyleUpdate(
        string style)
    {
        ButtonClass = style;
    }
}