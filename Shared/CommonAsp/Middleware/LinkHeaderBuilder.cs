using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonAsp.Middleware;

public class LinkHeaderBuilder
{
    private readonly int _perPage;
    private List<string> _links = new List<string>();
    private string _baseUri;

    public LinkHeaderBuilder(HttpRequest request, int perPage, string headerName="link")
    {
        _perPage = perPage;
        HeaderName = headerName;
        var uri = new UriBuilder(request.Scheme,request.Host.Host);
        if(request.Host.Port.HasValue)
            uri.Port = request.Host.Port.Value;
        uri.Path = request.Path;
        _baseUri = uri.Uri.ToString();
     }

    public void Add(int pageNo, Rel rel)
    {
        _links.Add($"<{_baseUri}?per_page={_perPage}&page={pageNo}>; rel=\"{RelToString(rel)}\"");
    }

    private string RelToString(Rel rel) =>
        rel switch
        {
            Rel.First => "first",
            Rel.Previous => "prev",
            Rel.Next => "next",
            Rel.Last => "last",
            _ => ""
        };

    public KeyValuePair<string,StringValues> ToHeader()
    {
        return new KeyValuePair<string, StringValues>
            (HeaderName,_links.ToArray());
    }

    public StringValues ToValues()
    {
        return _links.ToArray();
    }

    public string HeaderName { get; }

    public enum Rel
    {
        First,
        Previous,
        Next,
        Last
    }

}
