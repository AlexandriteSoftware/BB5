namespace BB5.Examples.Components;

public partial class InputPasswordBuilder
{
    private FormControlSize Size { get; set; } =
        FormControlSize.Default;

    private bool ReadOnly { get; set; }
    private FormControlSize InputPasswordSize { get; set; }
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
        InputPasswordSize = Enum.Parse<FormControlSize>(value);
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