namespace BB5.Examples.Components;

public class InputTextProperties
{
    public ComponentSize Size { get; set; } =
        ComponentSize.Default;
    
    public bool ReadOnly { get; set; } = false;
}

public partial class InputTextBuilder
{
    private InputTextProperties Properties { get; set; } = new();

    private string? InputTextClass { get; set; } = "";

    private void HandleStyleUpdate(
        string style)
    {
        InputTextClass = style;
    }
}