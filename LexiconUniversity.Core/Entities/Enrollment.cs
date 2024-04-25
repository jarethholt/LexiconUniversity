namespace LexiconUniversity.Core.Entities;

public class Enrollment
{
    public int Grade { get; set; }

    // Foreign keys
    public int CourseId { get; set; }
    public int StudentId { get; set; }

    // Navigation properties
    public Student Student { get; set; }
    public Course Course { get; set; }
}
