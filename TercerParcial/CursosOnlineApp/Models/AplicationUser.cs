using CursosOnlineApp.Models;
using Microsoft.AspNetCore.Identity;

public class ApplicationUser : IdentityUser<int>
{
    public string Nombre { get; set; } = null!;
    public DateTime FechaRegistro { get; set; } = DateTime.Now;

    // Relaciones - igual que en Usuario
    public ICollection<Curso> CursosImpartidos { get; set; } = new List<Curso>();
    public ICollection<Inscripcion> Inscripciones { get; set; } = new List<Inscripcion>();
    public ICollection<Comentario> Comentarios { get; set; } = new List<Comentario>();
    public ICollection<Reseña> Reseña { get; set; } = new List<Reseña>();
    public ICollection<ProgresoLeccion> Progresos { get; set; } = new List<ProgresoLeccion>();

    // Puedes mantener un campo Rol para algo extra, pero la gestión real es con IdentityRole
    public string Rol { get; set; } = "Estudiante";
}