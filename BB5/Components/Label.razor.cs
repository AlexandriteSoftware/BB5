using System.Collections.Generic;
using Microsoft.AspNetCore.Components;

namespace BB5.Components;

public partial class Label
{
    [Parameter]
    public string Content { get; set; } = "";
    
    [Parameter]
    public ContentType ContentType { get; set; } = ContentType.Text;

    [Parameter]
    public RenderFragment? ChildContent { get; set; }
    
    [Parameter]
    public string For { get; set; } = "";

    private string Classes { get; set; } = "";
    private Dictionary<string, object> Attributes { get; } = [];

    protected override void OnParametersSet()
    {
        base.OnParametersSet();
        
        var classList =
            new List<string>
            {
                "form-label"
            };

        Classes =
            string.Join(
                " ",
                classList);

        if (!string.IsNullOrEmpty(For))
            Attributes["for"] = For;
    }
}
