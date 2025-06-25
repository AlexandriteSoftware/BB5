namespace BB5.Examples.Components;

public class FormBuilderItem(
    string id)
{
    public string Id { get; } = id;
    
    public string Key { get; set; } = "";
    
    public string Value { get; set; } = "";
}

public partial class FormBuilder
{
    private string? FormClass { get; set; } = "";

    private object? FormItem { get; set; } =
        new FormBuilderItem("1")
        {
            Key = "Item 1",
            Value = "Description for Item 1"
        };

    protected override void OnInitialized()
    {
        base.OnInitialized();

        FormClass = "";
    }
}