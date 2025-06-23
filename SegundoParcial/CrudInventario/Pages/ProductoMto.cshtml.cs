using CrudInventario.Entity;
using CrudInventario.utils;
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

    public ActionResult OnPost(Producto producto)
    {
        string sql = "INSERT INTO productos (name,stock,price,descript) VALUES (@name,@stock,@price,@descript)";
        SqlConnector con = new SqlConnector("localhost", "storedotnet", "root", "Dayamoon12");
        con.Insert(sql, producto);
        TempData["Success"] = "Producto insertado correctamente.";
        
        return RedirectToPage("ProductoLista");
    }
}

