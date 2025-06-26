namespace BB5.Examples.Components;

public partial class InputTextBuilder
{
    private ComponentSize Size { get; set; } =
        ComponentSize.Default;

    private bool ReadOnly { get; set; }
    private ComponentSize InputTextSize { get; set; }
    private string? InputTextClass { get; set; } = "";
    private bool InputTextReadOnly { get; set; }

    protected override void OnInitialized()
    {
        base.OnInitialized();

        InputTextSize = Size;
        InputTextReadOnly = ReadOnly;
    }

    private void SizeUpdated(
        string value)
    {
        InputTextSize = Enum.Parse<ComponentSize>(value);
    }

    private void ReadOnlyUpdated(
        bool value)
    {
        InputTextReadOnly = value;
    }

    private void HandleStyleUpdate(
        string style)
    {
        InputTextClass = style;
    }
}