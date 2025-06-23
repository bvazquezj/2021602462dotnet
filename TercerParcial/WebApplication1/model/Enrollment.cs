using System.ComponentModel.DataAnnotations;
using WebApplication1.model;

public class Enrollment
{
    public int Id { get; set; }

    [Required]
    public int StudentId { get; set; }
    public Student Student { get; set; }

    [Required]
    public int CourseId { get; set; }
    public Course Course { get; set; }

    public DateTime EnrollmentDate { get; set; } = DateTime.Now;

    [MaxLength(5)]
    public string? Grade { get; set; } // Por ejemplo: A, B+, C-, etc.
}