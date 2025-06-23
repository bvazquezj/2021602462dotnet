using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CursosOnlineApp.Pages;

public class StartTeachModel : PageModel
{
    
    private readonly ILogger<StartTeachModel> _logger;

    public StartTeachModel(ILogger<StartTeachModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
       
    }
}