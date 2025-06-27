using Microsoft.AspNetCore.Components;

namespace BB5.Examples.Components;

public partial class ClassBuilder
{
    [Parameter]
    public EventCallback<string> OnClassUpdated { get; set; }

    private Shadow Shadow { get; set; } =
        Shadow.None;

    private Shadow SelectedShadow { get; set; }
    
    private string Class { get; set; } = "";

    protected override void OnInitialized()
    {
        base.OnInitialized();

        SelectedShadow = Shadow;
    }

    public async Task ClassUpdatedAsync(
        string value)
    {
        Class = value;
        await UpdateClassAsync();
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

        if (!string.IsNullOrEmpty(Class))
        {
            classes.AddRange(
                Class.Split(
                    ' ',
                    StringSplitOptions.RemoveEmptyEntries));
        }

        await OnClassUpdated.InvokeAsync(
            string.Join(
                " ",
                classes.Where(item => item != "")));
    }
}