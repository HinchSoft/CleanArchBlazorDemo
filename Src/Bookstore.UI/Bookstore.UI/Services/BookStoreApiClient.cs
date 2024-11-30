using Bookstore.UI.Services.Dtos;
using Microsoft.AspNetCore.Components.QuickGrid;
using System.Collections;
using System.Text;

namespace Bookstore.UI.Services;

public class BookStoreApiClient(HttpClient _httpClient)
{
    public async Task<int> GetAuthorsCountAsync(CancellationToken cancellationToken=default)
    {
        return await _httpClient.GetFromJsonAsync<int>("/authors?count=true",cancellationToken);
    }

    public async Task<ICollection<Author>> GetAuthorsAsync(int skip=0,int take=0,IEnumerable<SortedProperty>? sortorder = null, CancellationToken cancellationToken=default)
    {
        var url = new StringBuilder($"/authors?skip={skip}&take={take}");
        if (sortorder != null && sortorder.Count() > 0)
        {
            url.Append("&orderby=");
            url.AppendJoin(",",sortorder.Select(o=>$"{o.Direction}-{o.PropertyName}"));
        }

        var ret = new List<Author>();
        ret.AddRange( await _httpClient.GetFromJsonAsync<IEnumerable<Author>>(url.ToString(),cancellationToken));
        return ret;
    }

}
