namespace BB5.Examples.Components;

public class SelectProperties
{
    public ComponentSize Size { get; set; } =
        ComponentSize.Default;
    
    public int Rows { get; set; } = 4;
    
    public bool Multiple { get; set; } = false;
}

public partial class SelectBuilder
{
    private SelectProperties Properties { get; set; } = new();

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
    
    private void HandleStyleUpdate(
        string style)
    {
        SelectClass = style;
    }
}