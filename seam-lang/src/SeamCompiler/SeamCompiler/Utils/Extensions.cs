using System.Text;

namespace SeamCompiler.Utils;

internal static class Extensions
{
    public static string CollapseToString(this Either<string, StringBuilder> self)
        => self.Select(str => str, builder => builder.ToString());
}
