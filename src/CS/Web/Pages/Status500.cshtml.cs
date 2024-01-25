using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages;

public class Status500Model : PageModel
{
    private readonly ILogger<Status500Model> _logger;

    public Status500Model(ILogger<Status500Model> logger)
    {
        _logger = logger;
    }

    public IActionResult OnGet()
    {
        _logger.LogError("An error occurred while processing the request.");

        return StatusCode(StatusCodes.Status500InternalServerError);
    }
}
