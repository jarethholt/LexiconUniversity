using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace LexiconUniversity.Core.Entities;

[Index(nameof(Name), IsUnique = true)]
public class Course
{
    public int Id { get; set; }

    [StringLength(50, MinimumLength=1)]
    public string Name { get; set; } = default!;

    // Navigational properties
    public ICollection<Student> Students { get; set; } = [];
    public ICollection<Enrollment> Enrollments { get; set; } = [];
}
