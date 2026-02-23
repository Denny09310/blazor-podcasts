using System.Collections.Specialized;
using System.Web;

namespace Client.Models;

#pragma warning disable CS8618

public abstract class BaseRequest
{
    public int? Max { get; init; }

    public string? Lang { get; init; }
    public DateTime? Since { get; init; }

    public virtual NameValueCollection ToQuery()
    {
        var query = HttpUtility.ParseQueryString("");

        if (Max != null) query["max"] = $"{Max.Value}";
        if (Since != null) query["since"] = $"{((DateTimeOffset)Since).ToUnixTimeSeconds()}";
        if (Lang != null) query["lang"] = Lang;

        return query;
    }
}

#pragma warning restore CS8618