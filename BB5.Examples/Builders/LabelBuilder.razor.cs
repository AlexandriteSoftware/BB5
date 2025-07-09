using BB5.Models;

namespace BB5.Examples.Builders;

public partial class LabelBuilder
{
    private object? EditProperties { get; set; }

    private object? Properties { get; set; }

    private string Class { get; set; } = "";

    protected override void OnInitialized()
    {
        base.OnInitialized();

        EditProperties =
            new LabelProperties
            {
                Content = "Label"
            };

        Properties = EditProperties;
    }
}