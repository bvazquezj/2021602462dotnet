using System.ComponentModel.DataAnnotations;

namespace WebApplication1.model;

public class Course
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    [Range(1, 10)]
    public int Credits { get; set; }

    [Required]
    public int ProfessorId { get; set; }
    public Professor Professor { get; set; }

    public ICollection<Enrollment> Enrollments { get; set; }
}