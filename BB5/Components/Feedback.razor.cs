using System.Collections.Generic;
using Microsoft.AspNetCore.Components;

namespace BB5.Components;

public partial class Feedback
{
    [Parameter]
    public ValidationState ValidationState { get; set; }

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
            new List<string>();

        switch (ValidationState)
        {
            case ValidationState.Valid:
                classList.Add("valid-feedback");
                break;
            case ValidationState.Invalid:
                classList.Add("invalid-feedback");
                break;
            case ValidationState.None:
            default:
                break;
        }

        Classes =
            string.Join(
                " ",
                classList);
    }
}
