using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CrudInventario.Pages;

public class ProductiMtoModel : PageModel
{
    private readonly ILogger<ProductiMtoModel> _logger;

    public ProductiMtoModel(ILogger<ProductiMtoModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
    }

    public void OnPost(Producto producto)
    {
        

    }
}

