using System.Text.RegularExpressions;

namespace Client.Components.Pages;

public partial class Home
{
    [GeneratedRegex(@"<[^>]*>")]
    private static partial Regex GetHtmlTags();
}