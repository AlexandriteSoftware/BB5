using Microsoft.AspNetCore.Components;

namespace BB5.Examples.Components;

public partial class ClassBuilder
{
    [Parameter]
    public EventCallback<string> OnClassUpdated { get; set; }

    private Shadow Shadow { get; set; } =
        Shadow.None;

    private Shadow SelectedShadow { get; set; }

    protected override void OnInitialized()
    {
        base.OnInitialized();

        SelectedShadow = Shadow;
    }

    private async Task ShadowUpdatedAsync(
        string value)
    {
        SelectedShadow = Enum.Parse<Shadow>(value);
        await UpdateClassAsync();
    }

    private async Task UpdateClassAsync()
    {
        var classes =
            new List<string>
            {
                SelectedShadow switch
                {
                    Shadow.None => "",
                    Shadow.Small => "shadow-sm",
                    Shadow.Normal => "shadow",
                    Shadow.Large => "shadow-lg",
                    _ => ""
                }
            };

        await OnClassUpdated.InvokeAsync(
            string.Join(
                " ",
                classes.Where(item => item != "")));
    }
}