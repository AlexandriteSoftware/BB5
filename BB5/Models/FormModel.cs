using System.Collections.Generic;
using System.Linq;

namespace BB5.Models;

public class FormModel
{
    private readonly List<FormControlModel> _controls = [];

    public FormControlModel? Get(
        string name)
    {
        return _controls
            .FirstOrDefault(
                item =>
                    item.Name == name);
    }
    
    public void Add(
        FormControlModel control)
    {
        _controls.Add(control);
    }

    public void AddRange(
        IEnumerable<FormControlModel> control)
    {
        _controls.AddRange(control);
    }

    public void Clear()
    {
        _controls.Clear();
    }
    
    public IList<FormControlModel> ToList()
    {
        return _controls.ToList();
    }
}