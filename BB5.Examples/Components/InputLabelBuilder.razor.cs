using System.ComponentModel;

namespace BB5.Examples.Components;

public class InputLabelProperties
{
    public string Content { get; set; } = "";
    
    [DisplayName("Content is markup (HTML)")]
    public bool ContentIsMarkup { get; set; } = false;
}

public partial class InputLabelBuilder
{
    private object? EditProperties { get; set; }

    private object? Properties { get; set; }

    private string Class { get; set; } = "";

    protected override void OnInitialized()
    {
        base.OnInitialized();

        EditProperties =
            new InputLabelProperties
            {
                Content = "Label"
            };

        Properties = EditProperties;
    }
}