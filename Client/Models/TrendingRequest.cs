using System.Collections.Specialized;

namespace Client.Models;

#pragma warning disable CS8618

internal sealed class TrendingRequest : BaseRequest
{
    public string[] Cat { get; set; }

    public override NameValueCollection ToQuery()
    {
        var query = base.ToQuery();

        if (Cat != null && Cat.Length != 0) query["cat"] = string.Join(",", Cat);

        return query;
    }
}

#pragma warning restore CS8618