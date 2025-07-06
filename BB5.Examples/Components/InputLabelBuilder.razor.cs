using System.ComponentModel;

namespace BB5.Examples.Components;

public class InputLabelProperties
{
    public string Content { get; set; } = "";
    
    [DisplayName("Content is markup (HTML)")]
    public bool ContentIsMarkup { get; set; } = false;
    public bool Dismissible { get; set; }
}

public partial class InputLabelBuilder
{
    private InputLabelProperties Properties { get; set; } = new();
    private string? InputLabelClass { get; set; } = "";

    protected override void OnInitialized()
    {
        base.OnInitialized();

        Properties.Content = "Label";
        Properties.Dismissible = false;
    }

    private void HandleStyleUpdate(
        string style)
    {
        InputLabelClass = style;
    }
}