using Microsoft.AspNetCore.Components;

namespace BB5;

public partial class Icon
{
    /// <summary>
    ///     Icon id from https://icons.getbootstrap.com/.
    /// </summary>
    [Parameter]
    public string Id { get; set; } = "";

    private string Class { get; set; } = "";

    protected override void OnInitialized()
    {
        base.OnInitialized();
        Class = $"bi bi-{Id}";
    }
}