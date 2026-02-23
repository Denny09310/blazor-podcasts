using System.Text.RegularExpressions;

namespace Client.Helpers;

public static partial class Regexes
{
    public static string StripHtml(this string? input)
    {
        if (string.IsNullOrWhiteSpace(input)) return string.Empty;
        return GetHtmlTags().Replace(input, "");
    }

    [GeneratedRegex(@"<[^>]*>")]
    private static partial Regex GetHtmlTags();
}