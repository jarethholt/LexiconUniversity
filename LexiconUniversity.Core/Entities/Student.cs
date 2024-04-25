namespace LexiconUniversity.Core.Entities;

public class Student
{
    public int Id { get; set; }
    public string Avatar { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }

    public string FullName => $"{FirstName} {LastName}";

    // Navigational property
    public Address Address { get; set; }

    // 2 nullable foreign keys for creating many-to-many tables
    public ICollection<Course> Courses { get; set; }
}
