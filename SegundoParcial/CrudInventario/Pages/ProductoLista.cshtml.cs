﻿using System.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CrudInventario.Pages;

public class ProductoListaModel : PageModel
{
    public List<Producto>? Productos { get; set; }
    private readonly ILogger<ProductoListaModel> _logger;

    public ProductoListaModel(ILogger<ProductoListaModel> logger)
    {
        SqlConnector sql = new SqlConnector("localhost", "pruebas", "root", "Dayamoon12");
        DataSet ds = sql.GetDataSet("SELECT * FROM productos");
        Productos = new List<Producto>();
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            Producto producto = new Producto(){
                Id = Convert.ToInt32(row["id"]),
                Nombre = row["nombre"].ToString()!,
                Stock = Convert.ToInt32(row["stock"]),
                Precio = Convert.ToDecimal(row["precio"]),
                Descripcion = row["descripcion"].ToString()};
            Productos.Add(producto);
        }

        _logger = logger;
    }

    public void OnGet()
    {
    }

    public void OnPost()
    }
}
