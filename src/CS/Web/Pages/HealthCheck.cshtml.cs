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
        Dictionary<string, string> healthCheck = new Dictionary<string, string>();

        var httpClient = _httpClientFactory.CreateClient("API");
        var httpRequestMessageSql = new HttpRequestMessage(HttpMethod.Get, "GetProductCategory");

        var responseSql = await httpClient.SendAsync(httpRequestMessageSql);
        if (responseSql.IsSuccessStatusCode)
        {
            healthCheck.Add("SQL Database", "OK");
        }
        else
        {
            healthCheck.Add("SQL Database", "Invalid");
        }

        var httpRequestMessageBlob = new HttpRequestMessage(HttpMethod.Get, "GetBlobs");

        var responseBlob = await httpClient.SendAsync(httpRequestMessageBlob);
        if (responseBlob.IsSuccessStatusCode)
        {
            healthCheck.Add("Blob Storage", "OK");
        }
        else
        {
            healthCheck.Add("Blob Storage", "Invalid");
        }

        ViewData["HealthCheck"] = healthCheck;
    }
}