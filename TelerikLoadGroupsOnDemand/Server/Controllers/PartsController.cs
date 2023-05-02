namespace TelerikLoadGroupsOnDemand.Server.Controllers;

[ApiController, Route("[controller]")]
public class PartsController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Get([FromBody] DataSourceRequest request)
    {
        var parts = (await BuildSomePartsAsync()).ToList();

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

    /// <summary>
    /// Builds a fake <see cref="Part" /> list.
    /// </summary>
    private static async Task<IEnumerable<Part>> BuildSomePartsAsync()
    {
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
                CustomerNumber = i
            });
        }
        
        // Add a delay to simulate a slow response.
        await Task.Delay(1000);
        
        return parts;
    }
}