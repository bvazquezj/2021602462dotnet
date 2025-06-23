using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CursosOnlineApp.Pages;

public class IndexModel : PageModel
{
    public IActionResult OnGet()
    {
        if (User.Identity?.IsAuthenticated == true)
        {
            var rolPermitido = new[] { "Admin", "Profesor", "Estudiante" };

            // Obtener el primer rol (claim de tipo Role)
            var rolUsuario = User.Claims
                .FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.Role)?.Value;

            if (!string.IsNullOrEmpty(rolUsuario) && rolPermitido.Contains(rolUsuario))
            {
                return RedirectToPage($"/Dashboard/{rolUsuario}");
            }
        }

        // Si no está autenticado, no tiene rol o tiene un rol inválido
        return Page();
    }
}