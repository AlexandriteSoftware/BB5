namespace BB5.Models;

public class InputTextProperties
    : ISizeableComponent
{
    public string Placeholder { get; set; } = "";
    
    public ComponentSize Size { get; set; }
    
    public bool ReadOnly { get; set; }
}
