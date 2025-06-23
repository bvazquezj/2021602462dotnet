using System.ComponentModel.DataAnnotations;

namespace WebApplication1.model;

public class Professor
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string FullName { get; set; }

    [MaxLength(50)]
    public string Department { get; set; }

    public ICollection<Course> Courses { get; set; }
}