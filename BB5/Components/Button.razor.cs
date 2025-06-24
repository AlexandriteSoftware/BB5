using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace BB5.Components;

public partial class Button
{
    [Parameter]
    public AttentionState AttentionState { get; set; }

    [Parameter]
    public string Content { get; set; } = "";
    
    [Parameter]
    public ContentType ContentType { get; set; } = ContentType.Text;

    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    [Parameter]
    public EventCallback OnClick { get; set; }
    
    [Parameter]
    public bool Outline { get; set; }
    
    [Parameter]
    public ElementSize Size { get; set; }
    
    [Parameter]
    public bool Disabled { get; set; }

    private string Classes { get; set; } = "";
    private Dictionary<string, object> Attributes { get; } = [];

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        var classList =
            new List<string>
            {
                "btn"
            };
        
        var btnClass =
            Outline
                ? "btn-outline"
                : "btn";

        switch (AttentionState)
        {
            case AttentionState.Primary:
                classList.Add(btnClass + "-primary");
                break;
            case AttentionState.Secondary:
                classList.Add(btnClass + "-secondary");
                break;
            case AttentionState.Success:
                classList.Add(btnClass + "-success");
                break;
            case AttentionState.Danger:
                classList.Add(btnClass + "-danger");
                break;
            case AttentionState.Warning:
                classList.Add(btnClass + "-warning");
                break;
            case AttentionState.Info:
                classList.Add(btnClass + "-info");
                break;
            case AttentionState.Light:
                classList.Add(btnClass + "-light");
                break;
            case AttentionState.Dark:
                classList.Add(btnClass + "-dark");
                break;
            case AttentionState.None:
            default:
                break;
        }
        
        switch (Size)
        {
            case ElementSize.Small:
                classList.Add("btn-sm");
                break;
            case ElementSize.Large:
                classList.Add("btn-lg");
                break;
            case ElementSize.Normal:
            default:
                break;
        }
        
        Classes =
            string.Join(
                " ",
                classList);
        
        if (Disabled)
            Attributes.Add("disabled", "1");
    }

    private async Task HandleClickAsync()
    {
        await OnClick.InvokeAsync();
    }
}
