namespace CursosOnlineApp.Models;

public class ProgresoLeccion
{
    public int Id { get; set; }

    public int UsuarioId { get; set; }
    public ApplicationUser Usuario { get; set; } = null!;

    public int LeccionId { get; set; }
    public Leccion Leccion { get; set; } = null!;

    public bool Visto { get; set; } = false;
    public DateTime Fecha { get; set; } = DateTime.Now;
}