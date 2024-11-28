using Bookstore.UI.Services.Dtos;

namespace Bookstore.UI.Services;

public class BookStoreApiClient(HttpClient _httpClient)
{
    public async Task<int> GetAuthorsCountAsync(CancellationToken cancellationToken=default)
    {
        return await _httpClient.GetFromJsonAsync<int>("/authors?count=true",cancellationToken);
    }

    public async Task<ICollection<Author>> GetAuthorsAsync(int skip=0,int take=0, CancellationToken cancellationToken=default)
    {
        var ret = new List<Author>();
        ret.AddRange( await _httpClient.GetFromJsonAsync<IEnumerable<Author>>($"/authors?skip={skip}&take={take}",cancellationToken));
        return ret;
    }

}
