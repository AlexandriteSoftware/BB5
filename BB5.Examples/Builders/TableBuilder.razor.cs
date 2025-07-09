namespace BB5.Examples.Builders;

public class TableProperties;

public partial class TableBuilder
{
    private object? EditProperties { get; set; }

    private object? Properties { get; set; }

    private string Class { get; set; } = "";

    private object? Items { get; set; } =
        new[]
        {
            new { Id = 1, Name = "Item 1", Description = "Description for Item 1" },
            new { Id = 2, Name = "Item 2", Description = "Description for Item 2" },
            new { Id = 3, Name = "Item 3", Description = "Description for Item 3" }
        };

    protected override void OnInitialized()
    {
        base.OnInitialized();

        EditProperties =
            new TableProperties();
        
        Properties = EditProperties;
    }
}