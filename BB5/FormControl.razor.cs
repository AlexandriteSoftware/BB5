using BB5.Models;
using Microsoft.AspNetCore.Components;

namespace BB5;

public partial class FormControl
{
    [Parameter]
    public FormControlModel? Model { get; set; }
}