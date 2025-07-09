using System.ComponentModel;

namespace BB5.Examples.Builders;

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
    private object? EditProperties { get; set; }

    private object? Properties { get; set; }

    private string Class { get; set; } = "";

    protected override void OnInitialized()
    {
        base.OnInitialized();

        EditProperties =
            new ButtonProperties
            {
                Variant = ButtonVariant.Primary,
                Content = "Button"
            };

        Properties = EditProperties;
    }
}