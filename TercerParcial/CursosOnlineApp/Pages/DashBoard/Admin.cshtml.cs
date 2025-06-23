using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;

namespace CursosOnlineApp.Pages.DashBoard
{
    [Authorize(Roles = "Admin")]
    public class AdminModel : PageModel
    {
        // Aquí puedes agregar propiedades para estadísticas, usuarios, cursos, etc.
        // Ejemplo:
        // public List<Usuario> Usuarios { get; set; }
        // public List<Curso> CursosPendientes { get; set; }
        // public EstadisticasGenerales Estadisticas { get; set; }

        public void OnGet()
        {
            // Aquí puedes cargar los datos necesarios para el dashboard de administrador
        }
    }
}