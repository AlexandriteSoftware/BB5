namespace BB5.Examples.Components;

public partial class TableBuilder
{
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
}