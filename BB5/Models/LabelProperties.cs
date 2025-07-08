namespace BB5.Models;

public class LabelProperties
    : ISizeableComponent
{
    public string Content { get; set; } = "";
    
    public bool ContentIsMarkup { get; set; }
    
    public ComponentSize Size { get; set; }
}