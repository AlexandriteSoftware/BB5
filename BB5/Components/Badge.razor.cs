using System.Collections.Generic;
using Microsoft.AspNetCore.Components;

namespace BB5.Components;

public partial class Badge
{
    [Parameter]
    public Color Color { get; set; }

    [Parameter]
    public object? Content { get; set; }
    
    [Parameter]
    public ContentType ContentType { get; set; } = ContentType.Text;

    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    private string Classes { get; set; } = "";

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        var classList =
            new List<string>
            {
                "badge"
            };
        
        switch (Color)
        {
            case Color.Primary:
                classList.Add("text-bg-primary");
                break;
            case Color.Secondary:
                classList.Add("text-bg-secondary");
                break;
            case Color.Success:
                classList.Add("text-bg-success");
                break;
            case Color.Danger:
                classList.Add("text-bg-danger");
                break;
            case Color.Warning:
                classList.Add("text-bg-warning");
                break;
            case Color.Info:
                classList.Add("text-bg-info");
                break;
            case Color.Light:
                classList.Add("text-bg-light");
                break;
            case Color.Dark:
                classList.Add("text-bg-dark");
                break;
            case Color.None:
            default:
                break;
        }

        Classes =
            string.Join(
                " ",
                classList);
    }
}
