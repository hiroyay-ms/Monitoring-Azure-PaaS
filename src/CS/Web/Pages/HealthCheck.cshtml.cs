using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Web.Pages;

public class HealthCheckModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IConfiguration _configuration;
    private readonly IHttpClientFactory _httpClientFactory;

    public HealthCheckModel(ILogger<IndexModel> logger, IConfiguration configuration, IHttpClientFactory httpClientFactory)
    {
        _logger = logger;
        _configuration = configuration;
        _httpClientFactory = httpClientFactory;
    }

    public async Task OnGet()
    {
        var httpClient = _httpClientFactory.CreateClient("API");
        var httpRequestMessageSql = new HttpRequestMessage(HttpMethod.Get, "GetProductCategory");

        var responseSql = await httpClient.SendAsync(httpRequestMessageSql);
        if (responseSql.IsSuccessStatusCode)
        {
            _logger.LogInformation("Connect function app to SQL Database is OK");
        }
        else
        {
            _logger.LogError("Connect function app to SQL Database is Invalid");

            throw new Exception("Connect function app to SQL Database is Invalid");
        }

        var httpRequestMessageBlob = new HttpRequestMessage(HttpMethod.Get, "GetBlob");

        var responseBlob = await httpClient.SendAsync(httpRequestMessageBlob);
        if (responseBlob.IsSuccessStatusCode)
        {
            _logger.LogInformation("Connect function app to Blob Storage is OK");
        }
        else
        {
            _logger.LogError("Connect function app to Blob Storage is Invalid");

            throw new Exception("Connect function app to Blob Storage is Invalid");
        }
    }
}