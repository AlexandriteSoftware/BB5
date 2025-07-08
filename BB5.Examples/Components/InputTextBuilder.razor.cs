using BB5.Models;

namespace BB5.Examples.Components;

public partial class InputTextBuilder
{
    private object? EditProperties { get; set; }

    private object? Properties { get; set; }

    private string Class { get; set; } = "";

    protected override void OnInitialized()
    {
        base.OnInitialized();

        EditProperties =
            new InputTextProperties();

        Properties = EditProperties;
    }
}