using System.ComponentModel.DataAnnotations;

namespace WebApplication1.model;

public class Student
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string FullName { get; set; }

    [DataType(DataType.Date)]
    public DateTime BirthDate { get; set; }
    
    public ICollection<Enrollment> Enrollments { get; set; }
}