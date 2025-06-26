namespace BB5.Examples.Components;

public class TableProperties;

public partial class TableBuilder
{
    private TableProperties Properties { get; set; } = new();

    private string? TableClass { get; set; } = "";

    private object? TableItems { get; set; } =
        new[]
        {
            new { Id = 1, Name = "Item 1", Description = "Description for Item 1" },
            new { Id = 2, Name = "Item 2", Description = "Description for Item 2" },
            new { Id = 3, Name = "Item 3", Description = "Description for Item 3" }
        };

    protected override void OnInitialized()
    {
        base.OnInitialized();

        TableClass = "";
    }
    
    private void HandleStyleUpdate(
        string style)
    {
        TableClass = style;
    }
}