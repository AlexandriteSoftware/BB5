namespace BB5.Examples.Components;

public partial class InputTextBuilder
{
    private FormControlSize Size { get; set; } =
        FormControlSize.Default;

    private bool ReadOnly { get; set; }
    private FormControlSize InputTextSize { get; set; }
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
        InputTextSize = Enum.Parse<FormControlSize>(value);
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