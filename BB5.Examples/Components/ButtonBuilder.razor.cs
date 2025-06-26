namespace BB5.Examples.Components;

public partial class ButtonBuilder
{
    private ButtonVariant Variant { get; set; } =
        ButtonVariant.Primary;

    private string Content { get; set; } =
        "Button";

    private bool Outline { get; set; }
    private ButtonVariant ButtonVariant { get; set; }
    private object? ButtonContent { get; set; }
    private string? ButtonClass { get; set; } = "";
    private bool ButtonOutline { get; set; }

    protected override void OnInitialized()
    {
        base.OnInitialized();

        ButtonVariant = Variant;
        ButtonContent = Content;
        ButtonOutline = Outline;
    }

    private void VariantUpdated(
        string? value)
    {
        ButtonVariant =
            Enum.Parse<ButtonVariant>(
                value ?? "Primary");
    }

    private void ContentUpdated(
        string value)
    {
        ButtonContent = value;
    }

    private void HandleStyleUpdate(
        string style)
    {
        ButtonClass = style;
    }

    private void OutlineUpdated(
        bool value)
    {
        ButtonOutline = value;
    }
}