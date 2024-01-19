using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

using Web.Models;

namespace Web.Pages.Orders;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IConfiguration _configuration;
    private readonly IHttpClientFactory _httpClientFactory;

    public IndexModel(ILogger<IndexModel> logger, IConfiguration configuration, IHttpClientFactory httpClientFactory)
    {
        _logger = logger;
        _configuration = configuration;
        _httpClientFactory = httpClientFactory;
    }

    public async Task OnGet()
    {
        List<SalesOrder> orders = new List<SalesOrder>();
        string jsonString = string.Empty;

        string functionName = "GetSalesOrder";
        
        var httpClient = _httpClientFactory.CreateClient("API");
        var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, functionName);
        httpRequestMessage.Headers.Add("x-functions-key", _configuration.GetValue<string>("FUNCTION_KEY"));

        var response = await httpClient.SendAsync(httpRequestMessage);

        if (response.IsSuccessStatusCode)
        {
            jsonString = await response.Content.ReadAsStringAsync();

            orders = JsonSerializer.Deserialize<List<SalesOrder>>(jsonString);
        }

        ViewData["SalesOrders"] = orders;
    }
}