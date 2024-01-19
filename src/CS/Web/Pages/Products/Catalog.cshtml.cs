using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

using Web.Models;

namespace Web.Pages.Products;

public class CatalogModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IConfiguration _configuration;
    private readonly IHttpClientFactory _httpClientFactory;

    public CatalogModel(ILogger<IndexModel> logger, IConfiguration configuration, IHttpClientFactory httpClientFactory)
    {
        _logger = logger;
        _configuration = configuration;
        _httpClientFactory = httpClientFactory;
    }

    [BindProperty(SupportsGet = true)]
    public string? Id { get; set; }

    public async Task OnGet()
    {
        List<Product> products = new List<Product>();
        string jsonString = string.Empty;

        if (string.IsNullOrEmpty(Id))
            return;

        string functionName = $"GetProducts?id={Id}";

        var httpClient = _httpClientFactory.CreateClient("API");
        var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, functionName);
        httpRequestMessage.Headers.Add("x-functions-key", _configuration.GetValue<string>("FUNCTION_KEY"));

        var response = await httpClient.SendAsync(httpRequestMessage);

        if (response.IsSuccessStatusCode)
        {
            jsonString = await response.Content.ReadAsStringAsync();

            products = JsonSerializer.Deserialize<List<Product>>(jsonString);
        }

        ViewData["ProductList"] = products;

    }
}