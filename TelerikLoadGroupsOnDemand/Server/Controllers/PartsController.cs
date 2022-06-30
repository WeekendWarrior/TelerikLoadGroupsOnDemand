using Microsoft.Extensions.Caching.Memory;

namespace TelerikLoadGroupsOnDemand.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class PartsController : ControllerBase
{
    private readonly IMemoryCache _memoryCache;

    public PartsController(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }
    
    [HttpPost]
    public async Task<IActionResult> Get([FromBody] DataSourceRequest request)
    {
        var parts = BuildSomeParts().ToList();

        var dataSourceResult = await parts.ToDataSourceResultAsync(request);
        
        var response = new GetPartsResponse
        {
            Success = true,
            TotalResults = dataSourceResult.Total,
            Data = request.Groups.Count == 0
                ? dataSourceResult.Data.Cast<Part>().ToList() 
                : new(),
            GroupedData = request.Groups.Count > 0 
                ? dataSourceResult.Data.Cast<AggregateFunctionsGroup>().ToList()
                : new()
        };

        return Ok(response);
    }

    private IEnumerable<Part> BuildSomeParts()
    {
        var cacheKey = $"{nameof(PartsController)}.{nameof(BuildSomeParts)}";
        
        // Available in cache?
        if (_memoryCache.TryGetValue(cacheKey, out IEnumerable<Part> cachedParts)) return cachedParts;

        // Generate new parts.
        var random = new Random();
        var parts = new List<Part>();
        var maxResults = random.Next(0, 200);
        
        for (var i = 0; i < maxResults; i++)
        {
            parts.Add(new Part
            {
                Cipn = $"CIPN{random.Next(0, 10)}",
                Mpn= $"CIPN{random.Next(0, 50)}",
                CustomerName = $"customer{random.Next(0, 4)}",
            });
        }
        
        // Cache for 60 minutes.
        _memoryCache.Set(cacheKey, parts, TimeSpan.FromMinutes(60));

        return parts;
    }
}