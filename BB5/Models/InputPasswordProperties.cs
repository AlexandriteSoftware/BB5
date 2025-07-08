namespace BB5.Models;

public class InputPasswordProperties
    : ISizeableComponent
{
    public ComponentSize Size { get; set; } =
        ComponentSize.Default;
    
    public bool ReadOnly { get; set; } = false;
}