using System.Reflection;

namespace SeamCompiler.Tests.Utilities;

internal static class EmbeddedResources
{
    private static readonly string root
        = "SeamCompiler.Tests";

    public static readonly EmbeddedResourceCategory LexicalAnalysis
        = new("LexicalAnalysis.Resources");

    private static readonly Dictionary<string, string> cache = [];

    public static string Get(this EmbeddedResourceCategory category, string item)
    {
        var path = $"{root}.{category.Path}.{item}";

        if (cache.TryGetValue(path, out var content))
            return content;

        using var stream = Assembly
            .GetExecutingAssembly()
            .GetManifestResourceStream(path);

        if (stream == null)
            return "";

        using var streamReader = new StreamReader(stream);

        var code = streamReader.ReadToEnd();

        cache[path] = code;

        return code;
    }

    public class EmbeddedResourceCategory(string name)
    {
        public string Path { get; } = name;
    }
}
