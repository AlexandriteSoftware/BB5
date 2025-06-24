using System.Collections.Generic;
using Microsoft.AspNetCore.Components;

namespace BB5.Components;

public partial class Badge
{
    [Parameter]
    public AttentionState AttentionState { get; set; }

    [Parameter]
    public string Content { get; set; } = "";
    
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
        
        switch (AttentionState)
        {
            case AttentionState.Primary:
                classList.Add("text-bg-primary");
                break;
            case AttentionState.Secondary:
                classList.Add("text-bg-secondary");
                break;
            case AttentionState.Success:
                classList.Add("text-bg-success");
                break;
            case AttentionState.Danger:
                classList.Add("text-bg-danger");
                break;
            case AttentionState.Warning:
                classList.Add("text-bg-warning");
                break;
            case AttentionState.Info:
                classList.Add("text-bg-info");
                break;
            case AttentionState.Light:
                classList.Add("text-bg-light");
                break;
            case AttentionState.Dark:
                classList.Add("text-bg-dark");
                break;
            case AttentionState.None:
            default:
                break;
        }

        Classes =
            string.Join(
                " ",
                classList);
    }
}
