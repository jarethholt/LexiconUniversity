namespace LexiconUniversity.Core.Entities;

public class Student
{
    public int Id { get; set; }
    public string Avatar { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    public string FullName => $"{FirstName} {LastName}";

    // Navigational property
    public Address Address { get; set; } = default!;

    // 2 nullable foreign keys for creating many-to-many tables
    public ICollection<Course> Courses { get; set; } = [];
    public ICollection<Enrollment> Enrollments { get; set; } = [];
}
