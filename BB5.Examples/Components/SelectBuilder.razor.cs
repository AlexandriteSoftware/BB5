using BB5.Models;

namespace BB5.Examples.Components;

public partial class SelectBuilder
{
    private object? EditProperties { get; set; }

    private object? Properties { get; set; }

    private string Class { get; set; } = "";

    private object? Items { get; set; } =
        new[]
        {
            "Item 1",
            "Item 2",
            "Item 3"
        };

    protected override void OnInitialized()
    {
        base.OnInitialized();

        EditProperties =
            new SelectProperties();
        
        Properties = EditProperties;
    }
}