namespace BB5.Examples.Components;

public partial class SelectBuilder
{
    private string? SelectClass { get; set; } = "";

    private object? SelectItems { get; set; } =
        new[]
        {
            "Item 1",
            "Item 2",
            "Item 3"
        };

    protected override void OnInitialized()
    {
        base.OnInitialized();

        SelectClass = "";
    }
}