namespace BB5.Models;

public class SelectProperties
{
    public ComponentSize Size { get; set; } =
        ComponentSize.Default;
    
    public int Rows { get; set; } = 4;
    
    public bool Multiple { get; set; } = false;
}