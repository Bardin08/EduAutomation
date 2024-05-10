using System.Text.RegularExpressions;

namespace EduAutomation.Application.Helpers;

public static partial class MarkdownHelpers
{
    public static string NormalizeLinkText(this string text)
        => BracketsRegex().Replace(text, "");

    [GeneratedRegex(@"\[(.*?)\]")]
    private static partial Regex BracketsRegex();
}
