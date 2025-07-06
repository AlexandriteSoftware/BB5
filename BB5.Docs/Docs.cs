namespace BB5.Docs;

public static class Docs
{
    private static readonly Lazy<IReadOnlyDictionary<string, string>> DocItems = 
        new(Read);

    public static IReadOnlyDictionary<string, string> Get()
    {
        return DocItems.Value;
    }

    public static IReadOnlyList<string> GetComponentNames()
    {
        var components =
            DocItems.Value
                .Keys
                .Where(IsComponentPath)
                .Select(ComponentPathToName)
                .OrderBy(item => item)
                .ToList();

        return components;
    }
    
    public static bool TryGetComponentArticle(
        string name,
        out string documentation)
    {
        if (DocItems.Value.TryGetValue(
                ComponentNameToPath(name),
                out var value))
        {
            documentation = value;
            return true;
        }

        documentation = "";
        return false;
    }

    private static string ComponentNameToPath(
        string name)
    {
        return $"BB5.Docs.Components.{name}.md";
    }
    
    private static string ComponentPathToName(
        string path)
    {
        var name =
            path
                .Replace(
                    "BB5.Docs.Components.",
                    "",
                    StringComparison.OrdinalIgnoreCase)
                .Replace(
                    ".md",
                    "",
                    StringComparison.OrdinalIgnoreCase);

        return name;
    }
    
    private static bool IsComponentPath(
        string path)
    {
        return path
                   .StartsWith(
                       "BB5.Docs.Components.",
                       StringComparison.OrdinalIgnoreCase)
               && path
                   .EndsWith(
                       ".md",
                       StringComparison.OrdinalIgnoreCase);
    }

    private static IReadOnlyDictionary<string, string> Read()
    {
        return typeof(Docs)
            .Assembly
            .GetManifestResourceNames()
            .Where(item =>
                item.EndsWith(
                    ".md",
                    StringComparison.Ordinal))
            .Select(item => (Path: item, Content: GetResource(item)))
            .ToDictionary(
                item => item.Path,
                item => item.Content);
    }
    
    private static string GetResource(
        string resourceName)
    {
        using var stream = typeof(Docs).Assembly.GetManifestResourceStream(resourceName);
        if (stream == null)
            throw new InvalidOperationException($"Resource '{resourceName}' not found.");
        
        using var reader = new StreamReader(stream);
        return reader.ReadToEnd();
    }
}