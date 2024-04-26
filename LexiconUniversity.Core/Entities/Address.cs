namespace LexiconUniversity.Core.Entities;

public class Address
{
    public int Id { get; set; }
    public string Street { get; set; } = string.Empty;
    public string ZipCode { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;

    // Foreign key
    public int StudentId { get; set; }

    // Navigation property
    public Student Student { get; set; } = default!;
}
