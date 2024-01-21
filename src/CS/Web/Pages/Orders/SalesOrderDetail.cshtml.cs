using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

using Web.Models;

namespace Web.Pages.Orders;

public class SalesOrderDetailModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IConfiguration _configuration;
    private readonly IHttpClientFactory _httpClientFactory;

    [BindProperty(SupportsGet = true)]
    public string? Id { get; set; }

    public SalesOrderDetailModel(ILogger<IndexModel> logger, IConfiguration configuration, IHttpClientFactory httpClientFactory)
    {
        _logger = logger;
        _configuration = configuration;
        _httpClientFactory = httpClientFactory;
    }

    public async Task OnGet()
    {
        List<SalesOrderDetail>? salesOrderDetails = new List<SalesOrderDetail>();
        string jsonString = string.Empty;

        if (string.IsNullOrEmpty(Id))
            return;

        string functionName = $"GetSalesOrderDetail?id={Id}";

        var httpClient = _httpClientFactory.CreateClient("API");
        var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, functionName);
        httpRequestMessage.Headers.Add("x-functions-key", _configuration.GetValue<string>("FUNCTION_KEY"));

        var response = await httpClient.SendAsync(httpRequestMessage);

        if (response.IsSuccessStatusCode)
        {
            jsonString = await response.Content.ReadAsStringAsync();

            salesOrderDetails = JsonSerializer.Deserialize<List<SalesOrderDetail>>(jsonString);
        }

        ViewData["SalesOrderDetails"] = salesOrderDetails;
    }
}