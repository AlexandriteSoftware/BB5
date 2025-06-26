namespace BB5.Examples.Components;

public partial class InputPasswordBuilder
{
    private ComponentSize Size { get; set; } =
        ComponentSize.Default;

    private bool ReadOnly { get; set; }
    private ComponentSize InputPasswordSize { get; set; }
    private string? InputPasswordClass { get; set; } = "";
    private bool InputPasswordReadOnly { get; set; }

    protected override void OnInitialized()
    {
        base.OnInitialized();

        InputPasswordSize = Size;
        InputPasswordReadOnly = ReadOnly;
    }

    private void SizeUpdated(
        string value)
    {
        InputPasswordSize = Enum.Parse<ComponentSize>(value);
    }

    private void ReadOnlyUpdated(
        bool value)
    {
        InputPasswordReadOnly = value;
    }

    private void HandleStyleUpdate(
        string style)
    {
        InputPasswordClass = style;
    }
}