using CursosOnlineApp.Models; // Add this if ApplicationUser is in the same namespace, or change to the correct namespace

namespace CursosOnlineApp.Models;

public class Comentario
{
    public int Id { get; set; }

    public int UsuarioId { get; set; }
    public ApplicationUser Usuario { get; set; } = null!;

    public int CursoId { get; set; }
    public Curso Curso { get; set; } = null!;

    public string Texto { get; set; } = null!;
    public DateTime Fecha { get; set; } = DateTime.Now;
}