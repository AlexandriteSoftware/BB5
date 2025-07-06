namespace BB5.Examples.Components;

public class InputTextAreaProperties
{
    public ComponentSize Size { get; set; } =
        ComponentSize.Default;
    
    public bool ReadOnly { get; set; } = false;
}

public partial class InputTextAreaBuilder
{
    private InputTextAreaProperties Properties { get; set; } = new();

    private string? InputTextAreaClass { get; set; } = "";

    private void HandleStyleUpdate(
        string style)
    {
        InputTextAreaClass = style;
    }
}