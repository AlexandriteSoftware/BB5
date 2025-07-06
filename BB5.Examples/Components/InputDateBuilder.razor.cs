namespace BB5.Examples.Components;

public class InputDateProperties
{
    public ComponentSize Size { get; set; } =
        ComponentSize.Default;
    
    public bool ReadOnly { get; set; } = false;
}

public partial class InputDateBuilder
{
    private InputDateProperties Properties { get; set; } = new();

    private string? InputDateClass { get; set; } = "";

    private void HandleStyleUpdate(
        string style)
    {
        InputDateClass = style;
    }
}