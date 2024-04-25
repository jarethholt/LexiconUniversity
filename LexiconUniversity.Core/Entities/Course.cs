namespace LexiconUniversity.Core.Entities;

public class Course
{
    public int Id { get; set; }
    public string Name { get; set; }

    // Navigational properties
    public ICollection<Student> Students { get; set; }
}
