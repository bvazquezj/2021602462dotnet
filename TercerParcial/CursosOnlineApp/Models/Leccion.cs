using System.ComponentModel.DataAnnotations.Schema;

namespace CursosOnlineApp.Models;

public class Leccion
{
    public int Id { get; set; }
    public string Titulo { get; set; } = null!;
    public string Contenido { get; set; } = null!;
    [NotMapped]
    public IFormFile? VideoFile { get; set; }
    public string? VideoUrl { get; set; }
    public int? Orden { get; set; }
    public int DuracionMinutos { get; set; } = 5;
    public int CursoId { get; set; }
    public Curso? Curso { get; set; }

    public ICollection<ProgresoLeccion> Progresos { get; set; } = new List<ProgresoLeccion>();
}