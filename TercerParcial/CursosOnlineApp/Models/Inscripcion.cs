namespace CursosOnlineApp.Models;

public class Inscripcion
{
    public int Id { get; set; }
    public int UsuarioId { get; set; }
    public ApplicationUser Usuario { get; set; } = null!;

    public int CursoId { get; set; }
    public Curso Curso { get; set; } = null!;

    public DateTime FechaInscripcion { get; set; } = DateTime.Now;
    public bool Completado { get; set; } = false;
}