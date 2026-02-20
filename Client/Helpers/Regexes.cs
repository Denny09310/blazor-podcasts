using System.Text.RegularExpressions;

namespace Client.Helpers;

public static partial class Regexes
{
    [GeneratedRegex(@"<[^>]*>")]
    public static partial Regex GetHtmlTags();
}
