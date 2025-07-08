namespace BB5.Examples.Components;

public class InputTextAreaProperties
{
    public ComponentSize Size { get; set; } =
        ComponentSize.Default;
    
    public bool ReadOnly { get; set; } = false;
}

public partial class InputTextAreaBuilder
{
    private object? EditProperties { get; set; }

    private object? Properties { get; set; }

    private string Class { get; set; } = "";

    protected override void OnInitialized()
    {
        base.OnInitialized();

        EditProperties =
            new InputTextAreaProperties();

        Properties = EditProperties;
    }
}