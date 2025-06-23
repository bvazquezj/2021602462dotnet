namespace CursosOnlineApp.Models;

public class Reseña
{
    public int Id { get; set; }

    public int UsuarioId { get; set; }
    public ApplicationUser Usuario { get; set; } = null!;

    public int CursoId { get; set; }
    public Curso Curso { get; set; } = null!;

    public int Calificacion { get; set; } // 1 a 5
    public string Comentario { get; set; } = null!;
    public DateTime Fecha { get; set; } = DateTime.Now;
}