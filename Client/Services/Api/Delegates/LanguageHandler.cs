using System.Globalization;
using System.Web;

namespace Client.Services;

internal sealed class LanguageHandler : DelegatingHandler
{
    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var uri = request.RequestUri!;

        var builder = new UriBuilder(uri);
        var query = HttpUtility.ParseQueryString(builder.Query);

        if (string.IsNullOrEmpty(query["lang"]))
            query["lang"] = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;

        builder.Query = query.ToString()!;
        request.RequestUri = builder.Uri;

        return base.SendAsync(request, cancellationToken);
    }
}
