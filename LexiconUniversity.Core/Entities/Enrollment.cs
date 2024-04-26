namespace LexiconUniversity.Core.Entities;

public class Enrollment
{
    // Foreign keys
    public int CourseId { get; set; }
    public int StudentId { get; set; }

    // Navigation properties
    public Course Course { get; set; }
    public Student Student { get; set; }

    public int Grade { get; set; }
}
