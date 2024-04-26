using System.ComponentModel.DataAnnotations;

namespace LexiconUniversity.Core.Entities;

public class Student
{
    public int Id { get; set; }

    [DataType(DataType.ImageUrl)]
    public string Avatar { get; set; } = string.Empty;

    [Display(Name = "First name")]
    [StringLength(35, MinimumLength=1)]
    public string FirstName { get; set; } = string.Empty;

    [Display(Name = "Last name")]
    [StringLength(35, MinimumLength=1)]
    public string LastName { get; set; } = string.Empty;

    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Display(Name = "Full name")]
    public string FullName => $"{FirstName} {LastName}";

    public Address Address { get; set; } = default!;

    // 2 nullable foreign keys for creating many-to-many tables
    public ICollection<Course> Courses { get; set; } = [];
    public ICollection<Enrollment> Enrollments { get; set; } = [];
}
