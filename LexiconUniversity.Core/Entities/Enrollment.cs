namespace LexiconUniversity.Core.Entities;

public class Enrollment
{
    // Foreign keys
    public int CourseId { get; set; }
    public int StudentId { get; set; }

    // Navigation properties
    public Course Course { get; set; } = default!;
    public Student Student { get; set; } = default!;

    public int Grade { get; set; }
}
