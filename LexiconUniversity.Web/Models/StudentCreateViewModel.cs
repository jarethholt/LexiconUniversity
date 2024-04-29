namespace LexiconUniversity.Web.Models;

public class StudentCreateViewModel
{
    public string FirstName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string AddressStreet { get; init; } = string.Empty;
    public string AddressZipCode { get; init; } = string.Empty;
    public string AddressCity { get; init; } = string.Empty;

    public List<int> SelectedCourses { get; init; } = [];
}
