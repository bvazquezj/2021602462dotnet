using System.Collections;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppWeb2.Pages;

public class IndexModel : PageModel
{
    public ArrayList listaPersonas; 

    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
        
        listaPersonas = new ArrayList(
        new Persona[]{
            new Persona("John", 25, "123456789"),
            new Persona("Mary", 30, "987654321"),
            new Persona("Peter", 35, "456789123"),
            new Persona("Lucy", 40, "789123456"),
            new Persona("Paul", 45, "321654987")
        });

    }

    public void OnGet()
    {
        
    }
}
