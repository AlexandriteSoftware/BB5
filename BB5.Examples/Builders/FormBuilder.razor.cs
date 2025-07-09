using System.ComponentModel;

namespace BB5.Examples.Builders;

public class FormBuilderItem
{
    public string String { get; set; } = "";
    
    [TextArea]
    public string Text { get; set; } = "";

    [DisplayName("String (optional)")]
    public string? NullableString { get; set; }
    
    public DateOnly Date { get; set; }
    
    [DisplayName("Date (optional)")]
    public DateOnly? NullableDate { get; set; }

    public int Integer { get; set; }
    
    [DisplayName("Integer (optional)")]
    public int? NullableInteger { get; set; }
    
    public decimal Decimal { get; set; }
    
    [DisplayName("Decimal (optional)")]
    public decimal? NullableDecimal { get; set; }
    
    public bool Boolean { get; set; }
    
    [DisplayName("Boolean (optional)")]
    public bool? NullableBoolean { get; set; }
}

public class FormProperties;

public partial class FormBuilder
{
    private object? EditProperties { get; set; }

    private object? Properties { get; set; }

    private string Class { get; set; } = "";

    private object? Item { get; set; } =
        new FormBuilderItem
        {
            Date = DateOnly.FromDateTime(DateTime.Today)
        };

    protected override void OnInitialized()
    {
        base.OnInitialized();

        EditProperties =
            new FormProperties();
        
        Properties = EditProperties;
    }
}