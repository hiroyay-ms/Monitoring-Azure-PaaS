using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

using Web.Models;

namespace Web.Pages.Products;

public class  IndexModel : PageModel
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


    [BindProperty(SupportsGet = true)]
    public string? Id { get; set; }

    public async Task OnGet()
    {
        List<ProductCategory> productCategories = new List<ProductCategory>();
        string jsonString = string.Empty;

        string functionName = "GetProductCategory";
        if (!string.IsNullOrEmpty(Id))
        {
            functionName = $"{functionName}?id={Id}";
        }

        var httpClient = _httpClientFactory.CreateClient("API");
        var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, functionName);
        httpRequestMessage.Headers.Add("x-functions-key", _configuration.GetValue<string>("FUNCTION_KEY"));

        var response = await httpClient.SendAsync(httpRequestMessage);

        if (response.IsSuccessStatusCode)
        {
            jsonString = await response.Content.ReadAsStringAsync();

            productCategories = JsonSerializer.Deserialize<List<ProductCategory>>(jsonString);
        }

        ViewData["ProductCategories"] = productCategories;
    }
}