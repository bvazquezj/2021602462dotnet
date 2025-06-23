using System.ComponentModel.DataAnnotations;

namespace CursosOnlineApp.Models;

public class Curso
{
    public int Id { get; set; }
    [Required(ErrorMessage = "El título es obligatorio.")]
    public string Titulo { get; set; } = null!;
    [Required(ErrorMessage = "La descripción es obligatoria.")]
    public string Descripcion { get; set; } = null!;
    [Required(ErrorMessage = "La categoría es obligatoria.")]
    public string Categoria { get; set; } = null!;
    [Required(ErrorMessage = "El nivel es obligatorio.")]
    public string Nivel { get; set; } = null!;

    
    public string? ImagenUrl { get; set; }
    [Required(ErrorMessage = "El precio es obligatorio.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor que cero.")]
    public decimal Precio { get; set; }
    
    public DateTime FechaCreacion { get; set; } = DateTime.Now;

    public int InstructorId { get; set; }
    public ApplicationUser? Instructor { get; set; }

    public ICollection<Leccion> Lecciones { get; set; } = new List<Leccion>();
    public ICollection<Inscripcion> Inscripciones { get; set; } = new List<Inscripcion>();
    public ICollection<Comentario> Comentarios { get; set; } = new List<Comentario>();
    public ICollection<Reseña> Resenas { get; set; } = new List<Reseña>();
}