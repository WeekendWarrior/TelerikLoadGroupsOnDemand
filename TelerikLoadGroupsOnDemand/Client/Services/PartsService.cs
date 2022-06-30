using System.Net.Http.Json;
using Telerik.DataSource;
using TelerikLoadGroupsOnDemand.Shared;

namespace TelerikLoadGroupsOnDemand.Client.Services;

public class PartsService
{
    private readonly HttpClient _http;

    public PartsService(HttpClient http)
    {
        _http = http;
    }

    public async Task<GetPartsResponse> GetPartsAsync(DataSourceRequest request)
    {
        var response = await _http.PostAsJsonAsync("parts", request);
        
        var result = await response.Content.ReadFromJsonAsync<GetPartsResponse>();
        
        if (result == null) throw new ApplicationException("Failed to get parts");

        return result;
    }
}

