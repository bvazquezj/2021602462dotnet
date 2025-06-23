using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CursosOnlineApp.Pages;

public class ForBusinesModel : PageModel
{
    
    private readonly ILogger<ForBusinesModel> _logger;

    public ForBusinesModel(ILogger<ForBusinesModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
       
    }
}