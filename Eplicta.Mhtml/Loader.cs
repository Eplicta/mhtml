using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Eplicta.Mhtml;

public class Loader
{
    private readonly HttpClient _httpClient;

    //TODO: Provide options so that the correct httpClient with correct policy can be loaded.
    public Loader(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<PageData> Get(Uri page)
    {
        var message = await _httpClient.GetAsync(page);
        var stringContent = await message.Content.ReadAsStringAsync();

        stringContent = stringContent.Replace("\r", "").Replace("\n", "").Replace("\t", " ");
        //TODO: Download content here
        //TODO: Then download all resources
        return new PageData
        {
            MainContent = stringContent
        };
    }
}